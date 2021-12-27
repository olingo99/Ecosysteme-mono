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
        private List<Action> Moves;
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

        public override List<KeyValuePair<string, Entite>> GetPlay(Entite[,] matrix, plateau plateau)
        {
            List<KeyValuePair<string, Entite>> res = new List<KeyValuePair<string, Entite>>();

            double test = 0.8 * maxEp;
            if (EnForme())
            {
                SeekMate(matrix);
            }
            else
            {
                MoveRandom(matrix);
            }
            base.Checkpos(matrix);
            return res;
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
                        if (animal.GetEspece() == espece && animal.GetSex()!=sex)
                        {
                            double distance = Math.Sqrt((Math.Pow(animal.getPos(0) - posX, 2) + Math.Pow(animal.getPos(1) - posY, 2)));
                            if (distance<=rayonVision && distance >= rayonContact)//pas de && dans le if du dessus pour eviter de faire le calcul pour rien
                            {
                                //possibleMates.Add(new Tuple<int, int>(animal.getPos(0), animal.getPos(1)));
                                MoveToward(animal.getPos(0), animal.getPos(1));
                                //return;
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
                            //else
                            //{
                            //    MoveRandom(matrix);
                            //}
                        }
                    }
                }
            }
            //if (possibleMates.Count == 0)
            //{
            //    MoveRandom(matrix);
            //}
            //else
            //{
            //    MoveToward(possibleMates[0]);
            //}
            MoveRandom(matrix);
        }

        public bool EnForme()
        {
            return ep >= 0.8 * maxEp;
        }

        public void Impregnate()
        {
            pregnant = true;
            ep = 0;
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
            posX += rnd.Next(-10, 11);
            posY += rnd.Next(-10, 11);
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
                return "emptyheart";
            }
            else if (tempsRestantNaissance < 0.8* periodeGestation)
            {
                return "halfheart";
            }
            else
            {
                return "fullheart";
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
