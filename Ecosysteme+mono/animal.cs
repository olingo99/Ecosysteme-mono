using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Animal : EtreVivant
    {
        int periodeGestation, rayonContact, rayonVision;
        char sex;
        string type;
        List<Action> Moves;

        public Animal(int posX, int posY, int hp, int ep, int epLossSpeed,int speed, char sex, int periodeGestation, int rayonContact, int rayonVision,string type):base(posX, posY, hp, ep, epLossSpeed,speed) 
        {
            this.periodeGestation = periodeGestation;
            this.rayonContact = rayonContact;
            this.rayonVision = rayonVision;
            this.sex = sex;
            this.type = type;
           
        }

        public override void GetPlay(Entite[,] matrix)
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

        //private (string, int) CheckEating(Entite[,] matrix)
        //{
        //    for (var i = [this.posX - (rayonVision / 2), 0].Max(); i <= min(matrix.GetLength(0); i++)
        //    {
        //        for (var j = 0; j < matrix.GetLength(1); j++)
        //        {
        //            if (matrix[i, j] is nourriture)
        //            {

        //            }
        //        }
        //    }
        //    return ("test", 12);
        //}

        //public override void GetPlay(Entite[,] matrix)
        //{
        //    //Moves = new List<Action>(CheckEating(matrix));
        //    Dictionary<string, int> possibleMoves = new Dictionary<string, int>();
        //    //Entite[,] TroncatedMatrix = 
        //    (string key, int value )= CheckEating(matrix);
        //    possibleMoves.Add(key, value);
        //}

        
    }
}
