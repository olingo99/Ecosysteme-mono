using System;
using System.Collections.Generic;


namespace Ecosysteme_mono
{
    class Animal : EtreVivant
    {
        private int periodeGestation, rayonContact, rayonVision, speed, tempsRestantNaissance, tempsJeune, counter, damage;
        private char sex;
        private string type, espece;
        private bool pregnant;


        public Animal(int posX, int posY, int hp, int ep, int epLossSpeed,int speed, char sex, int periodeGestation, int rayonContact, int rayonVision,int damage,string type, string espece):base(posX, posY, hp, ep, epLossSpeed) 
        {
            this.periodeGestation = periodeGestation;
            this.rayonContact = rayonContact;
            this.rayonVision = rayonVision;
            this.sex = sex;
            this.type = type;
            this.speed = speed;
            this.espece = espece;
            this.pregnant = false;
            this.damage = damage;
            tempsJeune = (int)(periodeGestation * 1.5);
            counter = 0;
        }


        public override double GetPlay(Entite[,] matrix, Plateau plateau)
        {
            int index;
            counter++;
            if (tempsJeune > 0)
            {
                tempsJeune--;
            }
            if (hp <= 0)
            {
                index = plateau.GetIndexEtre(this);
                plateau.DeleteAnimal(this);
                return index;
            }
            else if (IsPregnant() && tempsRestantNaissance == 0)
            {
                plateau.AddAnimal(Naissance(matrix));
                EndTurn(matrix);
                return double.PositiveInfinity;
            }
            else if (EnForme())
            {
                SeekMate(matrix);
            }
            else
            {
                Tuple<bool, Nourriture> foodIsInRange = FoodInRange(matrix);
                if (foodIsInRange.Item1)
                {
                    if (foodIsInRange.Item2.GetType() == "viande")
                    {
                        ep = maxEp;
                    }
                    else
                    {
                        ep += damage;
                    }
                    plateau.DeleteNourriture(foodIsInRange.Item2);
                    
                    EndTurn(matrix);
                    return double.PositiveInfinity;
                }
                else
                {
                    SeekFood(matrix);
                }

            }

            if (IsPregnant())
            {
                tempsRestantNaissance--;
            }

            EndTurn(matrix);

            if (counter>=20)
            {
                counter = 0;
                Nourriture newNourriture = new Nourriture(posX, posY, "dechetOrga");
                
                newNourriture.Checkpos(matrix);
                plateau.AddNourriture(newNourriture);
            }
            return double.PositiveInfinity;
        }


        private void SeekMate(Entite[,] matrix)
        {
            List<Tuple<int, int>> possibleMates = new List<Tuple<int, int>>();
            for (int i = Math.Max(posX - (rayonVision), 0); i <= Math.Min(posX + (rayonVision), matrix.GetLength(0)-1); i++)
            {
                for (int j = Math.Max(posY - (rayonVision), 0); j <= Math.Min(posY + (rayonVision), matrix.GetLength(1)-1); j++)
                {
                    if (matrix[i, j] is Animal && matrix[i,j]!=this)
                    {
                        Animal animal = (Animal) matrix[i, j];
                        if (!pregnant && animal.GetEspece() == espece && animal.GetSex()!=sex && !(animal.IsPregnant()) && tempsJeune==0 && animal.GetTempsJeune()==0)
                        {
                            double distance = Math.Sqrt((Math.Pow(animal.GetPos(0) - posX, 2) + Math.Pow(animal.GetPos(1) - posY, 2)));
                            if (distance<=rayonVision && distance >= rayonContact)//pas de && dans le if du dessus pour eviter de faire le calcul pour rien
                            {
                                MoveToward(animal.GetPos(0), animal.GetPos(1));
                                Checkpos(matrix);
                            }
                            else if(distance <= rayonVision && distance <= rayonContact && animal.EnForme())
                            {
                                if(sex == 'f')
                                {
                                    Impregnate();
                                    return;
                                }
                                else
                                {
                                    animal.Impregnate();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            SeekFood(matrix);
        }


        private void SeekFood(Entite[,] matrix)
        {
            Nourriture closestFood = null;
            EtreVivant target = null;
            double smallestDistanceFood = double.PositiveInfinity;
            double smallestDistanceTarget = double.PositiveInfinity;
            for (int i = Math.Max(posX - (rayonVision), 0); i <= Math.Min(posX + (rayonVision), matrix.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(posY - (rayonVision), 0); j <= Math.Min(posY + (rayonVision), matrix.GetLength(1) - 1); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        if (nourriture.GetType() == "viande" && type == "carnivore")
                        {
                            double distance = Math.Sqrt((Math.Pow(nourriture.GetPos(0) - posX, 2) + Math.Pow(nourriture.GetPos(1) - posY, 2)));
                            if (distance < smallestDistanceFood && distance <= rayonVision)
                            {
                                closestFood = nourriture;
                                smallestDistanceFood = distance;
                            }
                        }
                    }
                    else if ((matrix[i,j] is Plante && type == "herbivore")||(matrix[i, j] is Animal && type == "carnivore" && matrix[i, j]!=this))
                    {
                        EtreVivant Etre = (EtreVivant) matrix[i, j];
                        double distance = Math.Sqrt((Math.Pow(Etre.GetPos(0) - posX, 2) + Math.Pow(Etre.GetPos(1) - posY, 2)));
                        if (distance <= rayonVision && distance < smallestDistanceTarget)
                        {
                            target = Etre;
                            smallestDistanceTarget = distance;
                        }
                    }
                }
            }
            if (closestFood != null)
            {
                MoveToward(closestFood.GetPos(0), closestFood.GetPos(1));
                Checkpos(matrix);
                return;
            }
            else if(target != null)
            {
                Attack(matrix, target, smallestDistanceTarget);
            }
            else
            {
                MoveRandom(matrix);
                Checkpos(matrix);
            }
        }



        private void Attack(Entite[,] matrix, EtreVivant target, double distance)
        {
            if (distance<= rayonContact)
            {
                if (target is Plante)
                {
                    ep+= (int)Math.Ceiling((decimal)maxEp / 2);
                }
                target.Hit(damage);
            }
            else
            {
                MoveToward(target.GetPos(0),target.GetPos(1)) ;
                Checkpos(matrix);
            }
        }


        private Tuple<bool, Nourriture> FoodInRange(Entite[,] matrix)
        {
            for (int i = Math.Max(posX - (rayonContact), 0); i <= Math.Min(posX + (rayonContact), matrix.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(posY - (rayonContact), 0); j <= Math.Min(posY + (rayonContact), matrix.GetLength(1) - 1); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        if (nourriture.GetType() == "viande" && type == "carnivore")
                        {
                            double distance = Math.Sqrt((Math.Pow(nourriture.GetPos(0) - posX, 2) + Math.Pow(nourriture.GetPos(1) - posY, 2)));
                            if (distance < rayonContact)
                            {
                                return new Tuple<bool, Nourriture>(true, nourriture);
                            }
                        }
                    }
                }
            }
            return new Tuple<bool, Nourriture>(false, null);
        }

        private Animal Naissance(Entite[,] matrix)
        {
            Random rnd = new Random();
            string newSex = "hf";
            pregnant = false;
            Animal newAnimal = new Animal(posX, posY, hp, ep, epLossSpeed, speed,(char) newSex[rnd.Next(0,2)], periodeGestation, rayonContact, rayonVision, damage,type, espece);
            newAnimal.Checkpos(matrix);
            tempsJeune = periodeGestation;
            return newAnimal;
        }
        public bool EnForme()
        {
            return ep >= 0.8 * maxEp;
        }

        public int GetTempsJeune()
        {
            return tempsJeune;
        }
        public void Impregnate()
        {
            pregnant = true;
            ep = (int)0.1*maxEp;
            tempsRestantNaissance = periodeGestation;
        }

        private void MoveToward(Tuple<int, int> toPos)
        {
            MoveToward(toPos.Item1, toPos.Item2);
        }
        private void MoveToward(int toPosX, int toPosY)
        {
            int distanceX = toPosX - posX;
            int distanceY = toPosY - posY;
            int j = Math.Min(speed, Math.Abs(distanceX));
            if (distanceX > 0)
            {
                posX += Math.Min(speed, Math.Abs(distanceX));
            }
            else
            {
                posX -= Math.Min(speed, Math.Abs(distanceX));
            }
            int i = Math.Min(speed, Math.Abs(distanceY));
            if (distanceY > 0)
            {
                posY += Math.Min(speed, Math.Abs(distanceY));
            }
            else
            {
                posY -= Math.Min(speed, Math.Abs(distanceY));
            }
        }

        private void MoveRandom(Entite[,] matrix)
        {
            Random rnd = new Random();
            posX += rnd.Next(-speed, speed+1);
            posY += rnd.Next(-speed, speed+1);
            if (posX >= matrix.GetLength(0))
            {
                posX = matrix.GetLength(0) - 1;
            }
            else if (posX < 0)
            {
                posX = 0;
            }
            if (posY >= matrix.GetLength(1))
            {
                posY = matrix.GetLength(1) - 1;
            }
            else if (posY < 0)
            {
                posY = 0;
            }
        }
        public int GetSpeed()
        {
            return this.speed;
        }

        public override string GetTexture()
        {
            return espece;
        }

        public string GetEspece()
        {
            return espece;
        }

        public char GetSex()
        {
            return sex;
        }


        public bool IsPregnant()
        {
            return pregnant;
        }

        public string GetPregnancyStatus()
        {
            //pas de switch case car pas possible de comparaison >/< en c# 8.0
            if (tempsRestantNaissance < 0.3 * periodeGestation)
            {
                return "fullheart";
            }
            else if (tempsRestantNaissance < 0.8* periodeGestation)
            {
                return "halfheart";
            }
            else
            {
                return "emptyheart";
            }
        }
    }
}
