using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ecosysteme_mono
{
    class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D HealthBar;
        private plateau plateau;
        private List<Plante> ToDrawPlante;
        private List<Animal> ToDrawAnimal;
        private List<Nourriture> ToDrawNourriture;
        private float scale;
        private HashSet<string> Textures;
        private Dictionary<string, Texture2D> TexturesDict;

        public Game1(plateau plateau)
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
            scale = 0.05f;
        }


        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 2500;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1300;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            HealthBar = Content.Load<Texture2D>("healthbar");

            ToDrawAnimal.ForEach(etre =>
            {
                Textures.Add(etre.GetTexture());
            });
            ToDrawPlante.ForEach(etre =>
            {
                Textures.Add(etre.GetTexture());
            });
            Textures.Add("viande");
            Textures.Add("dechetOrga");

            foreach(string texture in Textures)
            {
                TexturesDict.Add(texture, Content.Load<Texture2D>(texture));
            }
            //dinoTexture = Content.Load<Texture2D>("dino");
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
            System.Threading.Thread.Sleep(2000);
            GraphicsDevice.Clear(Color.SandyBrown);

            _spriteBatch.Begin();

            ToDrawAnimal.ForEach(etre => {
                _spriteBatch.Draw(TexturesDict[etre.GetTexture()], new Vector2(etre.getPos(0) * 10, etre.getPos(1) * 10), Color.White);

                _spriteBatch.Draw(HealthBar, new Rectangle(etre.getPos(0) * 10, TexturesDict[etre.GetTexture()].Height+etre.getPos(1) * 10, etre.GetCurrentHp(), 5), Color.Red);
            });

                

            

            ToDrawPlante.ForEach(plante =>
            {
                _spriteBatch.Draw(TexturesDict[plante.GetTexture()], new Vector2(plante.getPos(0) * 10, plante.getPos(1) * 10), Color.White);
            });
            ToDrawNourriture.ForEach(nourriture =>
            {
                _spriteBatch.Draw(TexturesDict[nourriture.GetTexture()], new Vector2(nourriture.getPos(0) * 10, nourriture.getPos(1) * 10), Color.White);
            });

            _spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
    }
}
