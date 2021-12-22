using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{

    class nourriture
    {
        private int taille, posX, posY,decayTime;
        private string type;


        public nourriture(int x, int y, int taille, string type)
        {
            posX = x;
            posY = y;
            this.taille = taille;
            this.type = type;
            decayTime = 10;
        }

        
        public string Type { get => type; set => type = value; }

        public void decay()
        {
            decayTime--;
            if (decayTime == 0)
            {
                type = "dechetOrga";
            }
        }

        public int getPos(int dim)
        {
            return dim == 0 ? posX : posY;
        }

    }
}
