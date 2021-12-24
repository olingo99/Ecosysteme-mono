using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class plateau
    {
        //nouvelle branches
        private Entite[,] matrix;
        private List<EtreVivant> listEtre;
        private List<Nourriture> listNourriture;
        private int sizeX, sizeY;
        private int counter = 0;



        public plateau(int sizeX, int sizeY, List<EtreVivant> list, List<Nourriture> listNourriture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.matrix = new EtreVivant[sizeX, sizeY];
            this.listEtre = SortBySpeed(list);
            this.listNourriture = listNourriture;
            UpdateMatrix();
        }
        private List<EtreVivant> SortBySpeed(List<EtreVivant> list)
        {
            return list.OrderByDescending(x => x.GetSpeed()).ToList();
        }

        public List<EtreVivant> GetListEtre()
        {
            return listEtre;
        }
        public List<Nourriture> GetListNourriture()
        {
            return listNourriture;
        }
        public void Play()
        {
            foreach (EtreVivant etre in listEtre)
            {
                etre.GetPlay(matrix);
                UpdateMatrix();
            }
        }

        //public void Play()
        //{
        //    listEtre[counter].GetPlay(matrix);
        //    UpdateMatrix();
        //    counter++;
        //    if (counter >= listEtre.Count)
        //    {
        //        counter = 0;
        //    }
        //}

        public void AddEtre(EtreVivant etre)
        {
            listEtre.Add(etre);
            listEtre = SortBySpeed(listEtre);
            UpdateMatrix();
        }
        public void AddNourriture(Nourriture Nourriture)
        {
            listNourriture.Add(Nourriture);
            UpdateMatrix();
        }

        public void DeleteEtre(EtreVivant etre)
        {
            listEtre.Remove(etre);
            UpdateMatrix();
        }
        public void DeleteNourriture(Nourriture Nourriture)
        {
            listNourriture.Remove(Nourriture);
            UpdateMatrix();
        }

        private void UpdateMatrix()
        {
            this.matrix = new Entite[sizeX, sizeY];
            foreach (EtreVivant etre in listEtre)
            {
                matrix[etre.getPos(0), etre.getPos(1)] = etre;
            }
            foreach (Nourriture Nourriture in listNourriture)
            {
                matrix[Nourriture.getPos(0), Nourriture.getPos(1)] = Nourriture;
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
