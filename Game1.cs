using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Birds.Code.Controller.CreateBird;
using Birds.Code.Controller.HeroMove;
using Birds.Code.Model.State;
using Birds.Code.View.FinalScreen;
using Birds.Code.View.SplashScreen;
using Birds.Code;


namespace Birds
{
    public class Game1 : Game
    {
        private bool isPaused = false;
        private bool isShooting = false;
        public static bool gameFinished = true;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Stat Stat = Stat.SplashScreen;
        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
            Main.Init(_spriteBatch, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Fire.LoadContent(Content);           
            SplashScreen.LoadContent(Content);
            FinalScreen.LoadContent(Content);
            Patrons.LoadContent(Content);
            GameScreen.LoadContent(Content);
            Hero.LoadContent(Content);
            Bird.LoadContent(Content);
            Main.LoadContent(Content);
            InformationScreen.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            /* Мышь */
            MouseState mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            Main.Update(gameTime);
            base.Update(gameTime);

            /* Клавиатура */
            keyboardState = Keyboard.GetState();
            Dictionary<Keys, Action> keyActions = new()
            {
                { Keys.Escape, () => Stat = Stat.SplashScreen },
                { Keys.W, () => Main.Hero.Move(MovingDirection.Up) },
                { Keys.S, () => Main.Hero.Move(MovingDirection.Down) },
                { Keys.D, () => Main.Hero.Move(MovingDirection.Right) },
                { Keys.A, () => Main.Hero.Move(MovingDirection.Left) }
            };

            if ((Stat == Stat.SplashScreen || Stat == Stat.Final) && MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Play(SplashScreen.menuSong);
            else if (Stat == Stat.Game) MediaPlayer.Stop();

            /* Загрузка экранов */
            switch (Stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    Main.Birds.Clear();
                    Patrons.Patron.Clear();
                    Main.Score = 0;
                    Main.PatrScore = 21;
                    if (mouseState.LeftButton == ButtonState.Pressed && SplashScreen.startButtonRect.Contains(mousePoint))
                    {
                        Stat = Stat.Game;
                        Main.IsGameRunning = true;
                        SplashScreen.MenuClick.Play();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && SplashScreen.exitButtonRect.Contains(mousePoint))
                    {
                        SplashScreen.MenuClick.Play();
                        Exit();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && SplashScreen.infoButtonRect.Contains(mousePoint))
                    {
                        Stat = Stat.Info;
                        SplashScreen.MenuClick.Play();
                    }
                    break;
                case Stat.Game:

                    if (Main.IsGameRunning && !isPaused)
                    {
                        Main.Update(gameTime);
                        GameScreen.Update();
                    }

                    if (mouseState.LeftButton == ButtonState.Pressed && !isShooting && Main.PatrScore > 0) // Проверка на патроны
                    {
                        Main.HeroFire();
                        isShooting = true;
                        Fire.ShootSound.Play();
                    }
                    else if (mouseState.LeftButton == ButtonState.Released)
                        isShooting = false;

                    foreach (var kvp in keyActions)
                    {
                        if (keyboardState.IsKeyDown(kvp.Key))
                            kvp.Value();
                    }
                    Main.Hero.RotateTowardsMouse(mouseState);
                    break;
                case Stat.Final:
                    FinalScreen.Update();
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        Stat = Stat.SplashScreen;
                        Main.IsGameRunning = false;
                        foreach (Bird bird in Main.Birds)
                            bird.RandomSet();
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && FinalScreen.backButtonRect.Contains(mousePoint))
                    {
                        Stat = Stat.SplashScreen;
                        Main.IsGameRunning = false;
                        SplashScreen.MenuClick.Play();
                    }
                    break;
                case Stat.Info:
                    if (mouseState.LeftButton == ButtonState.Pressed && InformationScreen.backButtonRect.Contains(mousePoint))
                    {
                        Stat = Stat.SplashScreen;
                        Main.IsGameRunning = false;
                        SplashScreen.MenuClick.Play();
                    }
                    break;
            }

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                isPaused = true; // Изменение состояния приостановки игры
            }

            if (isPaused)
            {
                return;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch (Stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Draw(_spriteBatch);
                    break;  
                case Stat.Game:
                    GameScreen.Draw(_spriteBatch);
                    Main.Draw();
                    break;
                case Stat.Final:
                    FinalScreen.Draw(_spriteBatch);
                    break;
                case Stat.Info:
                    InformationScreen.Draw(_spriteBatch);
                    break;
            }        
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
