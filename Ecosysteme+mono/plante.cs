using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Plante:EtreVivant
    {

        int rayonRacine, rayonSemis;
        string espece;

        public Plante(int posX, int posY, int hp,int ep, int epLossSpeed, int rayonRacine, int rayonSemis, string espece):base(posX,posY,hp,ep,epLossSpeed)
        {
            this.rayonRacine = rayonRacine;
            this.rayonSemis = rayonSemis;
            this.espece = espece;
        }

        public override string GetTexture()
        {
            return espece;
        }


        public override double GetPlay(Entite[,] matrix, plateau plateau)
        {
            double index;
            if (hp <= 0)
            {
                index = plateau.GetIndexEtre(this);
                plateau.DeletePlante(this);

                return index;
                //return new KeyValuePair<string, Entite>("delete", this);
            }
            else if (ep >= (0.8 * maxEp))
            {
                Random rnd = new Random();

                for (int i =0; i<rnd.Next(1,3); i++)
                {
                    plateau.AddPlante(reproduce(matrix));
                }
                
                EndTurn(matrix);
                return double.PositiveInfinity;
                //return new KeyValuePair<string, Entite>("add", reproduce(matrix));
            }
            else
            {
                Tuple<bool, Nourriture> CheckFood = foodInRange(matrix);
                if (CheckFood.Item1)
                {
                    plateau.DeleteNourriture(CheckFood.Item2);
                    ep +=(int)(maxEp * 0.5);
                    EndTurn(matrix);
                    return double.PositiveInfinity;
                    //return new KeyValuePair<string, Entite>("remove", CheckFood.Item2);
                }
            };
            EndTurn(matrix);
            return double.PositiveInfinity;
            //return new KeyValuePair<string, Entite>("", null);
        }

        //public override KeyValuePair<string, Entite> GetPlay(Entite[,] matrix, plateau plateau)
        //{
        //    if (hp <= 0)
        //    {

        //        return new KeyValuePair<string, Entite>("delete", this);
        //    }
        //    else if (ep >= 0.8*maxEp)
        //    {
        //        EndTurn(matrix);
        //        return new KeyValuePair<string, Entite>("add", reproduce(matrix));
        //    }
        //    else { 
        //        Tuple<bool, Nourriture> CheckFood = foodInRange(matrix);
        //        if (CheckFood.Item1)
        //        {
        //            EndTurn(matrix);
        //            return new KeyValuePair<string, Entite>("remove", CheckFood.Item2);
        //        }
        //    };
        //    EndTurn(matrix);
        //    return new KeyValuePair<string, Entite>("", null);
        //}

        private Tuple<bool, Nourriture> foodInRange(Entite[,] matrix)
        {
            for (int i = Math.Max(this.posX - (rayonRacine), 0); i <= Math.Min(this.posX + (rayonRacine), matrix.GetLength(0)-1); i++)
            {
                for (int j = Math.Max(this.posY - (rayonRacine), 0); j <= Math.Min(this.posY + (rayonRacine), matrix.GetLength(1)-1); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        double distance = Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2)));
                        if (nourriture.GetType() == "dechetOrga" && Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2))) <= rayonRacine)//empeche de faire une methode dans etrevivant car ion va devoir override donc de toute facon deux fois la premiere partie du code
                        {
                            //ep = maxEp;
                            return new Tuple<bool, Nourriture>(true, nourriture);
                        }
                    }
                }
            }
            return new Tuple<bool, Nourriture>(false, null);
        }

        private Plante reproduce(Entite[,]  matrix)
        {
            Random rnd = new Random();
            int newPosX = posX + rnd.Next(-rayonSemis, rayonSemis + 1);

            int newPosY = posY + rnd.Next(-rayonSemis, rayonSemis + 1);
            while ((Math.Sqrt((Math.Pow(newPosX - posX, 2) + Math.Pow(newPosY - posY, 2))) > rayonSemis) || newPosX<0 || newPosY<0 || newPosX>=matrix.GetLength(0)-1 || newPosY >= matrix.GetLength(1) - 1 || (matrix[newPosX, newPosY] is Entite))
            {
                newPosX = posX + rnd.Next(-rayonSemis, rayonSemis + 1);

                newPosY = posY + rnd.Next(-rayonSemis, rayonSemis + 1);
            }
            ep = (int)(maxEp*0.2);
            return new Plante(newPosX, newPosY, maxHp, maxEp, epLossSpeed, rayonRacine, rayonSemis,espece);
        }
    }
}