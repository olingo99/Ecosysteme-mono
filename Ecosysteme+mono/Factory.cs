using System;


namespace Ecosysteme_mono
{
    class Factory
    {
        Random rnd;
        Plateau plateau;
        int hp, ep, epLossSpeed, speed, periodeGestation, rayonContact, rayonVision, rayonRacine, rayonSemis, damage;
        string type, espece, sex;

        public Factory(Plateau plateau)
        {
            rnd = new Random();
            this.plateau = plateau;
            sex = "hf";
        }

        private int RandomAround(int stat, int ecartType)
        {
            return rnd.Next(stat - ecartType, stat + ecartType + 1);
        }
        public void CreateGiraffe(int posX, int posY)
        {
            damage = 25;
            hp = RandomAround(100, 15);
            ep = RandomAround(100, 15);
            epLossSpeed = RandomAround(2, 0);
            speed = RandomAround(5, 1);
            periodeGestation = RandomAround(20, 5);
            rayonContact = RandomAround(6, 1);
            rayonVision = RandomAround(40, 5);
            type = "herbivore";
            espece = "giraffe";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)],periodeGestation,rayonContact,rayonVision, damage, type,espece)); 
        }

        public void CreateDino(int posX, int posY)
        {
            damage = 40;
            hp = RandomAround(100, 15);
            ep = RandomAround(100, 15);
            epLossSpeed = RandomAround(3, 0);
            speed = RandomAround(15, 1);
            periodeGestation = RandomAround(20, 5);
            rayonContact = RandomAround(10, 1);
            rayonVision = RandomAround(60, 5);
            type = "carnivore";
            espece = "dino";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)], periodeGestation, rayonContact, rayonVision,damage, type, espece));

        }

        public void CreateMastodonte(int posX, int posY)
        {
            damage = 400;
            hp = RandomAround(500, 15);
            ep = RandomAround(200, 15);
            epLossSpeed = RandomAround(1, 0);
            speed = RandomAround(1, 0);
            periodeGestation = RandomAround(80, 5);
            rayonContact = RandomAround(2, 0);
            rayonVision = RandomAround(60, 5);
            type = "carnivore";
            espece = "mastodonte";
            plateau.AddAnimal(new Animal(posX, posY, hp, ep, epLossSpeed, speed, sex[rnd.Next(0, 2)], periodeGestation, rayonContact, rayonVision, damage, type, espece));

        }

        public void CreateBuisson(int posX, int posY)
        {

            hp = RandomAround(40, 15);
            ep = RandomAround(100, 15);
            epLossSpeed = RandomAround(3, 0);
            speed = RandomAround(10, 1);
            periodeGestation = RandomAround(20, 5);
            rayonRacine = RandomAround(10, 1);
            rayonSemis = RandomAround(15, 5);
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
