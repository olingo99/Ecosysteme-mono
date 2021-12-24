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
        private Texture2D dinoTexture, HealthBar;
        private Vector2 ballPosition;
        private Vector2 dinoPos;
        private float ballSpeed;
        private float dinoSpeed;
        private plateau plateau;
        private List<EtreVivant> ToDrawEtre;
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
            this.ToDrawEtre = plateau.GetListEtre();
            this.ToDrawNourriture = plateau.GetListNourriture();
            Textures = new HashSet<string>();
            TexturesDict = new Dictionary<string, Texture2D>();
            scale = 0.05f;
        }


        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 2500;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1300;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;
            dinoPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            dinoSpeed = 200f;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            HealthBar = Content.Load<Texture2D>("healthbar");

            ToDrawEtre.ForEach(etre =>
            {
                Textures.Add(etre.GetTexture());
            });
            ToDrawNourriture.ForEach(Nourriture =>
            {
                Textures.Add(Nourriture.GetTexture());
            });

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
            ToDrawEtre = plateau.GetListEtre();
            ToDrawNourriture = plateau.GetListNourriture();
            plateau.Play();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            ToDrawEtre.ForEach(etre => {
                _spriteBatch.Draw(TexturesDict[etre.GetTexture()], new Vector2(etre.getPos(0) * 10, etre.getPos(1) * 10), Color.White);

                _spriteBatch.Draw(HealthBar, new Rectangle(etre.getPos(0) * 10, TexturesDict[etre.GetTexture()].Height+etre.getPos(1) * 10, 50, 5), Color.Red);
                });

                

                ToDrawNourriture.ForEach(nourriture =>
                {
                    _spriteBatch.Draw(TexturesDict[nourriture.GetTexture()], new Vector2(nourriture.getPos(0) * 10, nourriture.getPos(1) * 10), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                });

                _spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
    }
}
