using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    abstract class Entite
{
        private protected int posX, posY;
        public Entite(int posX,int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public int getPos(int dim)
        {
            return dim == 0 ? posX : posY;
        }

        public Tuple<int,int> GetPos()
        {
            return new Tuple<int, int>(posX, posY);
        }

        public virtual string GetTexture()
        {
            return "default";
        }

        public void Checkpos(Entite[,] matrix)
        {
            Random rnd = new Random();
            while(matrix[posX,posY] is Entite && matrix[posX, posY] != this)
            {
                posX = posX > matrix.GetLength(0)/2 ? posX-1 : posX+1;
                posY = posY > matrix.GetLength(1)/2 ? posY - 1 : posY + 1;
            }
        }

        
    }


}
