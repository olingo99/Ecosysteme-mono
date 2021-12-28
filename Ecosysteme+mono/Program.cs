using System;

namespace Ecosysteme_mono
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {

            Plateau plat = new Plateau(250, 130);
            Factory factory = new Factory(plat);



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
