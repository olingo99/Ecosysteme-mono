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
        private Texture2D ballTexture;
        private Texture2D dinoTexture;
        private Vector2 ballPosition;
        private Vector2 dinoPos;
        private float ballSpeed;
        private float dinoSpeed;
        private plateau plateau;
        private List<EtreVivant> ToDraw;
        private float scale;

        public Game1(plateau plateau)
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.plateau = plateau;
            this.ToDraw = new List<EtreVivant>();
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

            //ballTexture = Content.Load<Texture2D>("ball");
            dinoTexture = Content.Load<Texture2D>("dino");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // TODO: Add your update logic here

            //var kstate = Keyboard.GetState();

            //if (kstate.IsKeyDown(Keys.Up))
            //    ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Down))
            //    ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Left))
            //    ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (kstate.IsKeyDown(Keys.Right))
            //    ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
            //    ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
            //else if (ballPosition.X < ballTexture.Width / 2)
            //    ballPosition.X = ballTexture.Width / 2;

            //if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
            //    ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
            //else if (ballPosition.Y < ballTexture.Height / 2)
            //    ballPosition.Y = ballTexture.Height / 2;
            ToDraw = plateau.GetList();
            plateau.Play();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            ToDraw.ForEach(etre => {
                _spriteBatch.Draw(dinoTexture, new Vector2(etre.getPos(0) * 10, etre.getPos(1) * 10), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                //_spriteBatch.Draw(dinoTexture, new Vector2(etre.getPos(0) * 10, etre.getPos(1) * 10), null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0f)

                int length = etre.GetHp() * 10;


                Texture2D rect = new Texture2D(_graphics.GraphicsDevice, length, 5);

                Color[] data = new Color[length * 5];
                for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
                rect.SetData(data);
                double testval = (double)dinoTexture.Height * scale;
                double testval2 = (double)etre.getPos(1) * 10;
                double a = testval + testval2;
                Vector2 coor = new Vector2(etre.getPos(0) * 10, (float)a);
                _spriteBatch.Draw(rect, coor, Color.White);

            }
            );


            //new Vector2(etre.getPos(0) * 100, etre.getPos(1) * 100)


            //            _spriteBatch.Draw(dinoTexture, dinoPos, null, Color.White, 0f,
            //Vector2.Zero, 0.1f, SpriteEffects.None, 0f);
            //            _spriteBatch.Draw(
            //    ballTexture,
            //    ballPosition,
            //    null,
            //    Color.White,
            //    0f,
            //    new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
            //    Vector2.One,
            //    SpriteEffects.None,
            //    0f
            //);
            ////            _spriteBatch.Draw(
            ////    dinoTexture,
            ////    dinoPos,
            ////    null,
            ////    Color.White,
            ////    0f,
            ////    new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
            ////    Vector2.One,
            ////    SpriteEffects.None,
            ////    0f, destinationRectangle
            ////);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
