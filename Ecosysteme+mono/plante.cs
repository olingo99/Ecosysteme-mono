using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Plante:EtreVivant
    {

        int rayonRacine, rayonSemis;

        public Plante(int posX, int posY, int hp,int ep, int epLossSpeed, int rayonRacine, int rayonSemis):base(posX,posY,hp,ep,epLossSpeed)
        {
            this.rayonRacine = rayonRacine;
            this.rayonSemis = rayonSemis;
        }

        public override string GetTexture()
        {
            return "plante";
        }



        public override List<KeyValuePair<string, Entite>> GetPlay(Entite[,] matrix, plateau plateau)
        {
            List<KeyValuePair<string, Entite>> res = new List<KeyValuePair<string, Entite>>();
            if (ep >= 0.8*maxEp)
            {
                res.Add(new KeyValuePair<string, Entite>("add", reproduce(matrix)));
            }
            else { 
                Tuple<bool, Nourriture> CheckFood = foodInRange(matrix);
                if (CheckFood.Item1)
                {
                    res.Add(new KeyValuePair<string, Entite>("remove", CheckFood.Item2));
                }
            };
            return res;
        }

        private Tuple<bool, Nourriture> foodInRange(Entite[,] matrix)
        {
            for (int i = Math.Max(this.posX - (rayonRacine / 2), 0); i <= Math.Min(this.posX + (rayonRacine / 2), matrix.GetLength(0)); i++)
            {
                for (int j = Math.Max(this.posY - (rayonRacine / 2), 0); j <= Math.Min(this.posY + (rayonRacine / 2), matrix.GetLength(1)); j++)
                {
                    if (matrix[i, j] is Nourriture)
                    {
                        Nourriture nourriture = (Nourriture)matrix[i, j];
                        if (nourriture.GetType() == "dechetOrga" && Math.Sqrt((Math.Pow(nourriture.getPos(0) - posX, 2) + Math.Pow(nourriture.getPos(1) - posY, 2))) <= rayonRacine)//empeche de faire une methode dans etrevivant car ion va devoir override donc de toute facon deux fois la premiere partie du code
                        {
                            ep = maxEp;
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
            while (Math.Sqrt((Math.Pow(newPosX - posX, 2) + Math.Pow(newPosY - posY, 2))) > rayonSemis && (!(matrix[newPosX, newPosY] is Entite)))
            {
                newPosX = posX + rnd.Next(-rayonSemis, rayonSemis + 1);

                newPosY = posY + rnd.Next(-rayonSemis, rayonSemis + 1);
            }
            ep = (int)0.5 * maxEp;
            return new Plante(newPosX, newPosY, maxHp, maxEp, epLossSpeed, rayonRacine, rayonSemis);
        }
    }
}
