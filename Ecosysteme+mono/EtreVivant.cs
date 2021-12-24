using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class EtreVivant:Entite
    {
        private protected int hp, ep, epLossSpeed, speed, maxHp;
        public EtreVivant(int posX, int posY, int hp, int ep, int epLossSpeed, int speed):base(posX, posY)
        {

            this.hp = hp;
            this.ep = ep;
            this.epLossSpeed = epLossSpeed;
            this.speed = speed;
            this.maxHp = hp;
        }

        public int GetCurrentHp()
        {
            return hp;
        }
        public int GetMaxHp()
        {
            return maxHp;
        }

        public override string ToString()
        {
            return " E";
        }
        public int GetSpeed()
        {
            return this.speed;
        }

        public virtual void GetPlay(Entite[,] matrix)
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

        public override string GetTexture()
        {
            return "dino";
        }
    }
}
