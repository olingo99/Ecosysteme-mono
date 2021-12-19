using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class EtreVivant
    {
        private int posX, posY, hp, ep, epLossSpeed, speed;
        public EtreVivant(int posX, int posY, int hp, int ep, int epLossSpeed, int speed)
        {
            this.posX = posX;
            this.posY = posY;
            this.hp = hp;
            this.ep = ep;
            this.epLossSpeed = epLossSpeed;
            this.speed = speed;
        }

        public int getPos(int dim)
        {
            return dim == 0 ? posX : posY;
        }

        public int GetHp()
        {
            return hp;
        }

        public override string ToString()
        {
            return " E";
        }
        public int GetSpeed()
        {
            return this.speed;
        }

        public void GetPlay(EtreVivant[,] matrix)
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

    }
}
