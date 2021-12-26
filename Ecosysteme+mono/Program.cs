using System;
using System.Collections.Generic;

namespace Ecosysteme_mono
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
            Animal test = new Animal(25,25,100,10,2,10,'h',5,10,20,"herbivore","dino");
            Animal test2 = new Animal(26, 26, 10, 10, 2, 10, 'f', 5, 10, 20, "herbivore","dino");
            test.ep = test.maxEp;
            test2.ep = test2.maxEp;
            //Animal test3 = new Animal(15, 15, 10, 2, 2, 10, 'f', 5, 5, 15, "herbivore","dino");
            List<Animal> l = new List<Animal>();

            //for (int i = 0; i < 22; i++)
            //{
            //    l.Add(new EtreVivant(i, i, 1, 1, 1, 1));
            //}
            l.Add(test);
            l.Add(test2);
            //l.Add(test3);
            List<Nourriture> ln = new List<Nourriture>();
            List<Plante> lp = new List<Plante>();
            ln.Add(new Nourriture(201, 111, 1, "dechetOrga"));
            lp.Add(new Plante(200, 110, 5, 2, 2, 15, 15));
            ln.Add(new Nourriture(0, 0, 2, "dechetOrga"));
            ln.Add(new Nourriture(0, 0, 2, "viande"));
            plateau plat = new plateau(250, 150, l,lp,ln);
            var game = new Game1(plat);
            game.Run();
        }
    }
}
