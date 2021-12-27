using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosysteme_mono
{
    class Factory
    {
        Random rnd;
        plateau plateau;
        int hp, ep, epLossSpeed, speed, periodeGestation, rayonContact, rayonVision, rayonRacine, rayonSemis, damage;
        string type, espece, sex;

        public Factory(plateau plateau)
        {
            rnd = new Random();
            this.plateau = plateau;
            sex = "hf";
        }

        private int randomAround(int stat, int ecartType)
        {
            return rnd.Next(stat - ecartType, stat + ecartType + 1);
        }
        public void CreateGiraffe(int posX, int posY)
        {
            damage = 25;
            hp = randomAround(100, 15);
            ep = randomAround(100, 15);
            epLossSpeed = randomAround(2, 0);
            speed = randomAround(5, 1);
            periodeGestation = randomAround(20, 5);
            rayonContact = randomAround(6, 1);
            rayonVision = randomAround(40, 5);
            type = "herbivore";
            espece = "giraffe";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)],periodeGestation,rayonContact,rayonVision, damage, type,espece)); 
        }

        public void CreateDino(int posX, int posY)
        {
            damage = 40;
            hp = randomAround(100, 15);
            ep = randomAround(100, 15);
            epLossSpeed = randomAround(3, 0);
            speed = randomAround(15, 1);
            periodeGestation = randomAround(20, 5);
            rayonContact = randomAround(10, 1);
            rayonVision = randomAround(60, 5);
            type = "carnivore";
            espece = "dino";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)], periodeGestation, rayonContact, rayonVision,damage, type, espece));

        }

        public void CreateMastodonte(int posX, int posY)
        {
            damage = 400;
            hp = randomAround(500, 15);
            ep = randomAround(200, 15);
            epLossSpeed = randomAround(1, 0);
            speed = randomAround(1, 0);
            periodeGestation = randomAround(80, 5);
            rayonContact = randomAround(2, 0);
            rayonVision = randomAround(60, 5);
            type = "carnivore";
            espece = "mastodonte";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)], periodeGestation, rayonContact, rayonVision, damage, type, espece));

        }

        public void CreateBuisson(int posX, int posY)
        {

            hp = randomAround(40, 15);
            ep = randomAround(100, 15);
            epLossSpeed = randomAround(3, 0);
            speed = randomAround(10, 1);
            periodeGestation = randomAround(20, 5);
            rayonRacine = randomAround(10, 1);
            rayonSemis = randomAround(15, 5);
            espece = "buisson";
            plateau.AddPlante(new Plante(posX,posY,hp,ep,epLossSpeed,rayonRacine,rayonSemis,espece));

        }

        public void CreateViande(int posX, int posY)
        {
            plateau.AddNourriture(new Nourriture(posX, posY, "viande"));
        }

        public void CreateDechetOrga(int posX, int posY)
        {
            plateau.AddNourriture(new Nourriture(posX, posY, "dechetOrga"));
        }
    }
}
