using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Animal : EtreVivant
    {
        private int periodeGestation, rayonContact, rayonVision, speed, tempsRestantNaissance;
        private char sex;
        private string type, espece;
        //private List<Action> Moves;
        private bool pregnant;

        public Animal(int posX, int posY, int hp, int ep, int epLossSpeed,int speed, char sex, int periodeGestation, int rayonContact, int rayonVision,string type, string espece):base(posX, posY, hp, ep, epLossSpeed) 
        {
            this.periodeGestation = periodeGestation;
            this.rayonContact = rayonContact;
            this.rayonVision = rayonVision;
            this.sex = sex;
            this.type = type;
            this.speed = speed;
            this.espece = espece;
            this.pregnant = false;

            //temptest
            
           
        }

        public override KeyValuePair<string, Entite> GetPlay(Entite[,] matrix, plateau plateau)
        {
            //List<KeyValuePair<string, Entite>> res = new List<KeyValuePair<string, Entite>>();

            double test = 0.8 * maxEp;
            if(hp <= 0)
            {
                return new KeyValuePair<string, Entite>("delete", this);
            }
            else if (IsPregnant() && tempsRestantNaissance == 0)
            {
                return new KeyValuePair<string, Entite>("add", Naissance(matrix));
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
                    ep += 10 * epLossSpeed;
                    return new KeyValuePair<string, Entite>("delete", foodIsInRange.Item2);
                }
                else
                {
                    SeekFood(matrix);
                }
                
            }
            base.Checkpos(matrix);
            if (ep > 0)
            {
                ep -= epLossSpeed;
            }
            else
            {
                hp -=(int)Math.Ceiling((decimal)maxHp / 10);
                ep = maxEp;
            }
            
            if (IsPregnant())
            {
                tempsRestantNaissance--;
            }
            return new KeyValuePair<string, Entite>("",null);
        }

        private void SeekMate(Entite[,] matrix)
        {
            List<Tuple<int, int>> possibleMates = new List<Tuple<int, int>>();
            for (int i = Math.Max(posX - (rayonVision / 2), 0); i <= Math.Min(posX + (rayonVision / 2), matrix.GetLength(0)-1); i++)
            {
                for (int j = Math.Max(posY - (rayonVision / 2), 0); j <= Math.Min(posY + (rayonVision / 2), matrix.GetLength(1)-1); j++)
                {
                    if (matrix[i, j] is Animal && matrix[i,j]!=this)
                    {
                        Animal animal = (Animal) matrix[i, j];
                        if (animal.GetEspece() == espece && animal.GetSex()!=sex && !(animal.IsPregnant()))
                        {
                            double distance = Math.Sqrt((Math.Pow(animal.getPos(0) - posX, 2) + Math.Pow(animal.getPos(1) - posY, 2)));
                            if (distance<=rayonVision && distance >= rayonContact)//pas de && dans le if du dessus pour eviter de faire le calcul pour rien
                            {
                                MoveToward(animal.getPos(0), animal.getPos(1));
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
            MoveRandom(matrix);
        }


        //private Tuple<bool,Nourriture> SeekFood(Entite[,] matrix)
        //{
        //    for (int i = Math.Max(posX - (rayonVision / 2), 0); i <= Math.Min(posX + (rayonVision / 2), matrix.GetLength(0) - 1); i++)
        //    {
        //        for (int j = Math.Max(posY - (rayonVision / 2), 0); j <= Math.Min(posY + (rayonVision / 2), matrix.GetLength(1) - 1); j++)
        //        {
        //            if (matrix[i, j] is Nourriture)
        //            {
        //                Nourriture nourriture = (Nourriture)matrix[i, j];
        //                if ((nourriture.GetType()=="dechetOrga" && type == "herbivore")|| (nourriture.GetType() == "viande" && type == "carnivore"))
        //                {
        //                    double distance = Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2)));
        //                    if (distance <= rayonVision && distance >= rayonContact)//pas de && dans le if du dessus pour eviter de faire le calcul pour rien
        //                    {
        //                        MoveToward(nourriture.getPos(0), nourriture.getPos(1));
        //                        return new Tuple<bool, Nourriture>(false, null);
        //                    }
        //                    else if (distance <= rayonVision && distance <= rayonContact)
        //                    {
        //                        return new Tuple<bool, Nourriture>(true, nourriture);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void SeekFood(Entite[,] matrix)
        {
            Nourriture closestFood = null;
            double smallestDistance = double.PositiveInfinity;
            for (int i = Math.Max(posX - (rayonVision / 2), 0); i <= Math.Min(posX + (rayonVision / 2), matrix.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(posY - (rayonVision / 2), 0); j <= Math.Min(posY + (rayonVision / 2), matrix.GetLength(1) - 1); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        if ((nourriture.GetType() == "dechetOrga" && type == "herbivore") || (nourriture.GetType() == "viande" && type == "carnivore"))
                        {
                            double distance = Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2)));
                            if (distance < smallestDistance && distance < rayonVision)
                            {
                                closestFood = nourriture;
                                smallestDistance = distance;
                            }
                        }
                    }
                }
            }
            if (closestFood != null)
            {
                MoveToward(closestFood.getPos(0), closestFood.getPos(1));
            }
            else
            {
                MoveRandom(matrix);
            }
        }

        private Tuple<bool, Nourriture> FoodInRange(Entite[,] matrix)
        {
            for (int i = Math.Max(posX - (rayonContact / 2), 0); i <= Math.Min(posX + (rayonContact / 2), matrix.GetLength(0) - 1); i++)
            {
                for (int j = Math.Max(posY - (rayonContact / 2), 0); j <= Math.Min(posY + (rayonContact / 2), matrix.GetLength(1) - 1); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        if ((nourriture.GetType() == "dechetOrga" && type == "herbivore") || (nourriture.GetType() == "viande" && type == "carnivore"))
                        {
                            double distance = Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2)));
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
            Animal newAnimal = new Animal(posX, posY, hp, ep, epLossSpeed, speed,(char) newSex[rnd.Next(0,2)], periodeGestation, rayonContact, rayonVision, type, espece);
            newAnimal.Checkpos(matrix);
            return newAnimal;
        }
        public bool EnForme()
        {
            return ep >= 0.8 * maxEp;
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
            posX += Math.Min(speed, toPosX - posX);
            posY += Math.Min(speed, toPosY - posY);
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
            return "dino";
        }

        public string GetEspece()
        {
            return espece;
        }

        public char GetSex()
        {
            return sex;
        }

        private void Reproduce()
        {
            return;
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

        //private (string, int) CheckEating(Entite[,] matrix)
        //{
        //    for (var i = [this.posX - (rayonVision / 2), 0].Max(); i <= min(matrix.GetLength(0); i++)
        //    {
        //        for (var j = 0; j < matrix.GetLength(1); j++)
        //        {
        //            if (matrix[i, j] is nourriture)
        //            {

        //            }
        //        }
        //    }
        //    return ("test", 12);
        //}

        //public override void GetPlay(Entite[,] matrix)
        //{
        //    //Moves = new List<Action>(CheckEating(matrix));
        //    Dictionary<string, int> possibleMoves = new Dictionary<string, int>();
        //    //Entite[,] TroncatedMatrix = 
        //    (string key, int value )= CheckEating(matrix);
        //    possibleMoves.Add(key, value);
        //}


    }
}
