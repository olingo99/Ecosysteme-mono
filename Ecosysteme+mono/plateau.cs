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
        private List<Animal> listAnimal;
        private List<Plante> listPlante;
        private List<Nourriture> listNourriture;
        private List<EtreVivant> listEtre;
        private int sizeX, sizeY;
        private int counter = 0;



        public plateau(int sizeX, int sizeY, List<Animal> list,List<Plante> listPlante, List<Nourriture> listNourriture)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.listPlante = listPlante;
            this.listAnimal = SortBySpeed(list);
            this.listNourriture = listNourriture;
            matrix = new EtreVivant[sizeX, sizeY];
            UpdateMatrix();
        }

        public plateau(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.listPlante = new List<Plante>();
            this.listAnimal = new List<Animal>();
            this.listNourriture =new List<Nourriture>();
            listEtre = new List<EtreVivant>();
            matrix = new EtreVivant[sizeX, sizeY];
            UpdateMatrix();
        }

        private List<Animal> SortBySpeed(List<Animal> list)
        {
            return list.OrderByDescending(x => x.GetSpeed()).ToList();
        }

        public List<Plante> GetListPlante()
        {
            return listPlante;
        }
        public List<Animal> GetListAnimal()
        {
            return listAnimal;
        }
        public List<Nourriture> GetListNourriture()
        {
            return listNourriture;
        }


        public void Play()
        {
            double index = double.PositiveInfinity;
            for(int i = 0; i < listEtre.Count(); i++)
            {
                index = listEtre[i].GetPlay(matrix, this);
                if (index <= i)
                {
                    i--;
                }
                UpdateMatrix();
            }
            foreach (Nourriture nourriture in listNourriture)
            {
                if (nourriture.GetType() == "viande")
                {
                    nourriture.decay();
                }
                UpdateMatrix();
            }
        }

        public int GetIndexEtre(EtreVivant etre)
        {
            return listEtre.IndexOf(etre);
        }

        //public void Play()
        //{
        //    List<KeyValuePair<string, Entite>> toDo = new List<KeyValuePair<string, Entite>>();
        //    foreach (Nourriture nourriture in listNourriture)
        //    {
        //        if (nourriture.GetType() == "viande")
        //        {
        //            nourriture.decay();
        //        }
        //    }
        //    DoChanges(toDo);
        //    toDo.Clear();
        //    foreach (Plante plante in listPlante)
        //    {
        //        KeyValuePair<string, Entite> play = plante.GetPlay(matrix, this);
        //        if (play.Key != "")
        //        {
        //            toDo.Add(play);
        //        }
        //        UpdateMatrix();
        //    }
        //    DoChanges(toDo);
        //    toDo.Clear();
        //    foreach (Animal etre in listAnimal)
        //    {
        //        KeyValuePair<string, Entite> play = etre.GetPlay(matrix, this);
        //        if (play.Key != "")
        //        {
        //            toDo.Add(play);
        //        }
        //        UpdateMatrix();
        //    }
        //    DoChanges(toDo);
        //}

        private void DoChanges(List<KeyValuePair<string, Entite>> toDo)
        {
            toDo.ForEach(elem =>                    //impossible de faire un switch case avec typeof
            {
                if (elem.Value.GetType()==typeof(Animal))
                {
                    if (elem.Key == "add")
                    {
                        AddAnimal((Animal)elem.Value);
                    }
                    else
                    {
                        DeleteAnimal((Animal)elem.Value);
                    }
                }
                else if (elem.Value.GetType() == typeof(Plante))
                {
                    if (elem.Key == "add")
                    {
                        AddPlante((Plante)elem.Value);
                    }
                    else
                    {
                        DeletePlante((Plante)elem.Value);
                    }

                }
                else if (elem.Value.GetType() == typeof(Nourriture))
                {
                    if (elem.Key == "add")
                    {
                        AddNourriture((Nourriture)elem.Value);
                    }
                    else
                    {
                        DeleteNourriture((Nourriture)elem.Value);
                    }

                }
            });
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

        public void AddAnimal(Animal etre)
        {
            listAnimal.Add(etre);
            //listAnimal = SortBySpeed(listAnimal);
            listEtre.Add(etre);
            UpdateMatrix();
        }
        public void AddNourriture(Nourriture Nourriture)
        {
            listNourriture.Add(Nourriture);
            UpdateMatrix();
        }

        public void AddPlante(Plante Plante)
        {
            listEtre.Add(Plante);
            listPlante.Add(Plante);
            UpdateMatrix();
        }

        public void DeletePlante(Plante Plante)
        {
            listPlante.Remove(Plante);
            listEtre.Remove(Plante);
            listNourriture.Add(new Nourriture(Plante.getPos(0), Plante.getPos(1), "dechetOrga"));
            UpdateMatrix();
        }
        public void DeleteAnimal(Animal Animal)
        {
            listAnimal.Remove(Animal);
            listEtre.Remove(Animal);
            listNourriture.Add(new Nourriture(Animal.getPos(0), Animal.getPos(1), "viande"));
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
            foreach (Animal etre in listAnimal)
            {
                matrix[etre.getPos(0), etre.getPos(1)] = etre;
            }
            foreach (Plante etre in listPlante)
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
