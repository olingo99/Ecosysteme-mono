using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class EtreVivant
    {
        private protected int posX, posY, hp, ep, epLossSpeed, speed;
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

        public virtual void GetPlay(EtreVivant[,] matrix)
        {
            
        }

    }
}
