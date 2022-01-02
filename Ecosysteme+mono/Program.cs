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
            factory.CreateBuisson(150, 110);
            factory.CreateBuisson(20, 80);
            factory.CreateDechetOrga(201, 111);
            factory.CreateDechetOrga(150, 111);
            factory.CreateDechetOrga(180, 111);
            factory.CreateGiraffe(205, 115);
            factory.CreateGiraffe(199, 115);
            factory.CreateGiraffe(190, 115);
            factory.CreateBuisson(191, 114);
            factory.CreateBuisson(192, 114);
            factory.CreateDino(10, 50);
            factory.CreateDino(30, 40);
            factory.CreateDino(1, 40);

            //factory.CreateMastodonte(50, 50);
            var game = new Game1(plat);
            game.Run();
        }
    }
}
