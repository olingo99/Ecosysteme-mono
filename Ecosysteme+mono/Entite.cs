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

        public virtual string GetTexture()
        {
            return "default";
        }


    }


}
