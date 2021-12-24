using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{

    class Nourriture:Entite
    {
        private int taille,decayTime;
        private string type;


        public Nourriture(int x, int y, int taille, string type): base(x,y)
        {
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

        public override string GetTexture()
        {
            return base.GetTexture();
        }

    }
}
