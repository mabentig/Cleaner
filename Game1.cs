using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
namespace Cleaner
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int roomNumber = 2;

        #region Inget att bry sig om!
        //----------------------------------------Variabler och objekt
        Random rng = new Random();

        public const int ScreenWidth = 1000;
        public const int ScreenHeight = 800;

        int elapsedSeconds;

        SpriteFont font;
        SoundEffect b, s;
        SoundEffectInstance bump, slurp;
        ObstacleManager obstacles;
        DirtManager dirtManager;
        TrackManager trackManager;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Cleaner cleaner;

        //----------------------------------------Egenskaper
        public int TimeLeft
        {
            get
            {
                return 300 - elapsedSeconds;
            }
        }

        //----------------------------------------Konstruktorer
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        //----------------------------------------Metoder
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.ApplyChanges();
            base.Initialize();

            cleaner = new Cleaner(new Vector2(200, 200), Content, "cleaner_small", Color.White);
            dirtManager = new DirtManager(Content);
            obstacles = new ObstacleManager(Content, roomNumber);
            trackManager = new TrackManager(Content);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            b = Content.Load<SoundEffect>("bump_wav");
            bump = b.CreateInstance();
            slurp = Content.Load<SoundEffect>("slurp_wav").CreateInstance();
            bump.IsLooped = false;
            slurp.IsLooped = true;
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (TimeLeft > 0)
            {
                elapsedSeconds = (int)gameTime.TotalGameTime.TotalSeconds;
                MouseState newMouseState = Mouse.GetState();
                if (newMouseState.LeftButton == ButtonState.Pressed)
                    for (int i = 0; i < dirtManager.dirts.Count; i++)
                    {
                        GameObject tile = dirtManager.dirts[i];
                        if (tile.Hitbox.Contains(newMouseState.Position))
                        {
                            tile.color = Color.Turquoise;
                            obstacles.obstacles.Add(tile);
                            dirtManager.dirts.RemoveAt(i);
                        }

                    }

                Algorithm();

                dirtManager.cleans.AddRange(cleaner.Update(dirtManager.dirts, obstacles.obstacles, trackManager));

                if (cleaner.IsColliding)
                {
                    b.CreateInstance().Play();
//                    if (bump.State == SoundState.Playing)
//                      bump.Stop();
   //                 bump.Play();
                }
                if (cleaner.IsDriving)
                    slurp.Play();
                else
                    slurp.Pause();

                slurp.Pitch=(float)rng.NextDouble()*2-1;

                base.Update(gameTime);
            }
    }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            dirtManager.Draw(spriteBatch);
            trackManager.Draw(spriteBatch);
            obstacles.Draw(spriteBatch);
            cleaner.Draw(spriteBatch);


            spriteBatch.DrawString(font, "Cleaned: " + dirtManager.Cleaned, new Vector2(50, 50), Color.Black);
            spriteBatch.DrawString(font, "Time left: " + TimeLeft, new Vector2(50, 80), Color.Black);
            spriteBatch.DrawString(font, "Distance: " + (trackManager.Count), new Vector2(50, 110), Color.Black);


            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion


        /// <summary>
        /// Här ska städalgoritmen skrivas!
        /// </summary>
        public void Algorithm()
        {
            if (cleaner.IsColliding)
            {
                cleaner.Reverse(20);
 //               if (!cleaner.IsRotating)
                    cleaner.Rotate(100 + rng.Next(-30, 30));
//                    cleaner.Rotate(100 * (2 * rng.Next(0, 2) - 1));
            }
            else if (!cleaner.IsRotating)
            {
                cleaner.Rotate(rng.Next(-60, 61));

                if (!cleaner.IsDriving)
                {
                    cleaner.Forward(50);
//                    if(rng.Next(10)==0)
//                        cleaner.Rotate(10 * rng.Next(-3, 4));// * (2 * rng.Next(0, 2) - 1));
                }

            }
        }
    }
}
