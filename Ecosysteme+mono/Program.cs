using System;
using System.Collections.Generic;

namespace Ecosysteme_mono
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
            //Animal test = new Animal(100,100,100,100,10,10,'h',5,10,100,40,"carnivore","dino");
            //Animal test2 = new Animal(202, 115, 10, 100, 5, 5, 'f', 25, 10, 100,40, "herbivore","giraffe");
            //Animal test3 = new Animal(203, 115, 10, 100, 5, 5, 'h', 25, 10, 100,40, "herbivore", "giraffe");
            //test.ep = test.maxEp;
            //test2.ep = test2.maxEp;
            //Animal test4 = new Animal(15, 15, 10, 2, 2, 10, 'f', 5, 5, 100,40, "carnivore","dino");
            //List<Animal> l = new List<Animal>();

            ////for (int i = 0; i < 22; i++)
            ////{
            ////    l.Add(new EtreVivant(i, i, 1, 1, 1, 1));
            ////}
            //l.Add(test);
            ////l.Add(test2);
            ////l.Add(test3);
            //List<Nourriture> ln = new List<Nourriture>();
            //List<Plante> lp = new List<Plante>();
            ////ln.Add(new Nourriture(201, 111, 1, "dechetOrga"));
            ////lp.Add(new Plante(200, 110, 5, 2, 2, 15, 15));
            ////ln.Add(new Nourriture(0, 0, 2, "dechetOrga"));
            
            ////ln.Add(new Nourriture(0, 0, 2, "viande"));
            
            plateau plat = new plateau(250, 130);
            Factory factory = new Factory(plat);
            //plat.AddAnimal(test);
            //plat.AddAnimal(test2);
            //plat.AddNourriture(new Nourriture(210, 111, "dechetOrga"));
            //plat.AddNourriture(new Nourriture(0, 0, "dechetOrga"));
            //plat.AddNourriture(new Nourriture(0, 0,  "viande"));
            //plat.AddPlante(new Plante(200, 110, 50, 100, 2, 15, 15, "buisson"));
            //plat.AddAnimal(test3);
            //plat.AddAnimal(test4);
            ////plateau plat = new plateau(250, 130, l,lp,ln);


            factory.CreateBuisson(205, 80);
            factory.CreateDechetOrga(201, 111);
            factory.CreateGiraffe(205, 115);
            //factory.CreateDino(10, 50);
            //factory.CreateMastodonte(50, 50);
            var game = new Game1(plat);
            game.Run();
        }
    }
}
