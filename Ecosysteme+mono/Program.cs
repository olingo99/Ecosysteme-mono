using System;
using System.Collections.Generic;

namespace Ecosysteme_mono
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            EtreVivant test = new EtreVivant(3, 3, 3, 3, 3, 3);
            EtreVivant test2 = new EtreVivant(9, 9, 9, 9, 3, 9);
            EtreVivant test3 = new EtreVivant(1, 1, 1, 1, 1, 1);
            List<EtreVivant> l = new List<EtreVivant>();
            //for (int i = 0; i < 22; i++)
            //{
            //    l.Add(new EtreVivant(i, i, 1, 1, 1, 1));
            //}
            l.Add(test);
            l.Add(test2);
            l.Add(test3);
            List<Nourriture> ln = new List<Nourriture>();
            ln.Add(new Nourriture(50, 50, 2, "dechet orga"));
            plateau plat = new plateau(250, 150, l,ln);
            var game = new Game1(plat);
            game.Run();
        }
    }
}
