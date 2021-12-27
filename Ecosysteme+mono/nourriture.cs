using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{

    class Nourriture:Entite
    {
        private int decayTime;
        private string type;


        public Nourriture(int x, int y, string type): base(x,y)
        {
            this.type = type;
            decayTime = 100;
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

        public new string GetType()
        {
            return type;
        }
        public override string GetTexture()
        {
            return type;
        }
    }
}
