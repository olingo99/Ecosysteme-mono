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




            //factory.CreateBuisson(206, 80);
            //factory.CreateBuisson(207, 80);
            //factory.CreateDechetOrga(201, 80);
            //factory.CreateDechetOrga(202, 80);
            //factory.CreateDechetOrga(203, 80);
            //factory.CreateGiraffe(192, 115);
            //factory.CreateGiraffe(191, 115);
            //factory.CreateGiraffe(190, 115);
            //plat.AddAnimal(new Animal(191, 80, 20, 100, 1, 10, 'h', 10, 50, 50, 10, "herbivore", "giraffe"));

            //plat.AddAnimal(new Animal(192, 80, 20, 100, 1, 10, 'f', 10, 50, 50, 10, "herbivore", "giraffe"));
            plat.AddPlante(new Plante(205, 80, 100, 100,10, 15, 20, "buisson"));
            //factory.CreateBuisson(205, 80);
            //factory.CreateDino(10, 50);
            //factory.CreateDino(30, 40);
            //factory.CreateDino(1, 40);

            //factory.CreateMastodonte(50, 50);
            var game = new Game1(plat);
            game.Run();
        }
    }
}
