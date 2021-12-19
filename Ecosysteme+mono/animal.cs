using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Animal : EtreVivant
    {
        int periodeGestation, rayonContact, rayonVision;
        char sex;

        public Animal(int posX, int posY, int hp, int ep, int epLossSpeed,int speed, char sex, int periodeGestation, int rayonContact, int rayonVision):base(posX, posY, hp, ep, epLossSpeed,speed) 
        {
            this.periodeGestation = periodeGestation;
            this.rayonContact = rayonContact;
            this.rayonVision = rayonVision;
            this.sex = sex;
        }

        public void play( int matrice)
        {
            return;
        }
    }
}
