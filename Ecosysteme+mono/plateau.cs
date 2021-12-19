using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class plateau
    {
        //nouvelle branches
        private EtreVivant[,] matrix;
        private List<EtreVivant> listEtre;
        private int sizeX, sizeY;



        public plateau(int sizeX, int sizeY, List<EtreVivant> list)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.matrix = new EtreVivant[sizeX, sizeY];
            this.listEtre = SortBySpeed(list);
        }
        private List<EtreVivant> SortBySpeed(List<EtreVivant> list)
        {
            return list.OrderByDescending(x => x.GetSpeed()).ToList();
        }

        public List<EtreVivant> GetList()
        {
            return listEtre;
        }
        public void Play()
        {
            foreach (EtreVivant etre in listEtre)
            {
                etre.GetPlay(matrix);
                UpdateMatrix();
                Console.WriteLine(this.ToString());
            }
        }

        public void AddEtre(EtreVivant etre)
        {
            listEtre.Add(etre);
            listEtre = SortBySpeed(listEtre);
        }

        public void DeleteEtre(EtreVivant etre)
        {
            listEtre.Remove(etre);
        }

        private void UpdateMatrix()
        {
            this.matrix = new EtreVivant[sizeX, sizeY];
            foreach (EtreVivant etre in listEtre)
            {

                matrix[etre.getPos(0), etre.getPos(1)] = etre;
            }
        }

        public override string ToString()
        {
            UpdateMatrix();
            var s = new StringBuilder();

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] is EtreVivant)
                    {
                        s.Append(matrix[i, j].ToString());
                    }
                    else
                    {
                        s.Append(" _");
                    }
                }

                s.AppendLine();
            }

            return s.ToString();
        }
    }
}
