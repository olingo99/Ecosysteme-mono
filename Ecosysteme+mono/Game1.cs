using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Ecosysteme_mono
{
    class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D HealthBar, EnergyBar;
        private Plateau plateau;
        private List<Plante> ToDrawPlante;
        private List<Animal> ToDrawAnimal;
        private List<Nourriture> ToDrawNourriture;
        private HashSet<string> Textures;
        private Dictionary<string, Texture2D> TexturesDict;

        public Game1(Plateau plateau)
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.plateau = plateau;
            ToDrawAnimal = plateau.GetListAnimal();
            ToDrawNourriture = plateau.GetListNourriture();
            ToDrawPlante = plateau.GetListPlante();
            Textures = new HashSet<string>();
            TexturesDict = new Dictionary<string, Texture2D>();
        }


        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 2500;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1300;   // set this value to the desired height of your window
            this.TargetElapsedTime = TimeSpan.FromSeconds((double)1/5);
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            HealthBar = Content.Load<Texture2D>("healthbar");
            EnergyBar = Content.Load<Texture2D>("energybar");

            Textures.Add("emptyheart");
            Textures.Add("halfheart");
            Textures.Add("fullheart");
            Textures.Add("viande");
            Textures.Add("dechetOrga");


            ToDrawAnimal.ForEach(etre =>
            {
                Textures.Add(etre.GetTexture());
            });
            ToDrawPlante.ForEach(etre =>
            {
                Textures.Add(etre.GetTexture());
            });

            
            foreach(string texture in Textures)
            {
                TexturesDict.Add(texture, Content.Load<Texture2D>(texture));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ToDrawPlante = plateau.GetListPlante();
            ToDrawAnimal = plateau.GetListAnimal();
            ToDrawNourriture = plateau.GetListNourriture();
            plateau.Play();
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            _spriteBatch.Begin();


            ToDrawPlante.ForEach(plante =>
            {
                _spriteBatch.Draw(TexturesDict[plante.GetTexture()], new Vector2(plante.GetPos(0) * 10, plante.GetPos(1) * 10), Color.White);

                _spriteBatch.Draw(HealthBar, new Rectangle(plante.GetPos(0) * 10, TexturesDict[plante.GetTexture()].Height + plante.GetPos(1) * 10, (int)(TexturesDict[plante.GetTexture()].Width * ((double)plante.GetCurrentHp() / plante.GetMaxHp())), 5), Color.Red);

                _spriteBatch.Draw(EnergyBar, new Rectangle(plante.GetPos(0) * 10, TexturesDict[plante.GetTexture()].Height + 5 + plante.GetPos(1) * 10, (int)(TexturesDict[plante.GetTexture()].Width * ((double)plante.GetCurrentEp() / plante.GetMaxEp())), 5), Color.Yellow);
            });


            ToDrawNourriture.ForEach(nourriture =>
            {
                _spriteBatch.Draw(TexturesDict[nourriture.GetTexture()], new Vector2(nourriture.GetPos(0) * 10, nourriture.GetPos(1) * 10), Color.White);
            });


            ToDrawAnimal.ForEach(etre => {
                _spriteBatch.Draw(TexturesDict[etre.GetTexture()], new Vector2(etre.GetPos(0) * 10, etre.GetPos(1) * 10), Color.White);

                _spriteBatch.Draw(HealthBar, new Rectangle(etre.GetPos(0) * 10, TexturesDict[etre.GetTexture()].Height+etre.GetPos(1) * 10, (int)(TexturesDict[etre.GetTexture()].Width* ((double)etre.GetCurrentHp()/etre.GetMaxHp())), 5), Color.Red);
                
                _spriteBatch.Draw(EnergyBar, new Rectangle(etre.GetPos(0) * 10, TexturesDict[etre.GetTexture()].Height + 5 + etre.GetPos(1) * 10, (int)(TexturesDict[etre.GetTexture()].Width * ((double)etre.GetCurrentEp() / etre.GetMaxEp())), 5), Color.Yellow);
                if (etre.IsPregnant())
                {
                    _spriteBatch.Draw(TexturesDict[etre.GetPregnancyStatus()], new Vector2((etre.GetPos(0) * 10)+ TexturesDict[etre.GetTexture()].Width, etre.GetPos(1) * 10), Color.White);
                }
            });

            _spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
            
        }
    }
}
