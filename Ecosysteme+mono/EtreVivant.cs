using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class EtreVivant:Entite
    {
        public int hp, ep, epLossSpeed, maxHp, maxEp;
        public EtreVivant(int posX, int posY, int hp, int ep, int epLossSpeed):base(posX, posY)
        {
            maxEp = ep;
            this.hp = hp;
            this.ep = (int)(ep*0.5);
            this.epLossSpeed = epLossSpeed;
            maxHp = hp;
            
        }

        public int GetCurrentHp()
        {
            return hp;
        }

        public int GetCurrentEp()
        {
            return ep;
        }

        public int GetMaxEp()
        {
            return maxEp;
        }
        public int GetMaxHp()
        {
            return maxHp;
        }

        public override string ToString()
        {
            return " E";
        }

        public void Hit(int damage)
        {
            hp -= damage;
        }
        
        protected void EndTurn(Entite[,] matrix)
        {
            base.Checkpos(matrix);
            if (ep > maxEp)
            {
                ep = maxEp;
                hp += (int)Math.Floor((decimal)maxHp / 5);
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
            }
            else if (ep > 0)
            {
                ep -= epLossSpeed;
            }
            else
            {
                hp -= (int)Math.Ceiling((decimal)maxHp / 10);
                ep = (int)(maxEp*0.5);
            }

            
        }

        public virtual double GetPlay(Entite[,] matrix, plateau plateau)
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
            return double.PositiveInfinity;
        }

        //public virtual KeyValuePair<string,Entite> GetPlay(Entite[,] matrix, plateau plateau)
        //{
        //    Random rnd = new Random();
        //    posX += rnd.Next(-10, 11);
        //    posY += rnd.Next(-10, 11);
        //    if (posX >= matrix.GetLength(0))
        //    {
        //        posX = matrix.GetLength(0) - 1;
        //    }
        //    else if (posX < 0)
        //    {
        //        posX = 0;
        //    }
        //    if (posY >= matrix.GetLength(1))
        //    {
        //        posY = matrix.GetLength(1) - 1;
        //    }
        //    else if (posY < 0)
        //    {
        //        posY = 0;
        //    }
        //    return new KeyValuePair<string, Entite>();
        //}


    }
}
