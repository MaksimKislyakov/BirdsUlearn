using System;
using Birds.Code;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Birds.Code.Controller.CreateBird;
using Birds.Code.Model.State;

namespace Birds
{
    class Main
    {
        public static Random rnd = new();
        public static SpriteBatch SpriteBatch { get; set; }
        public static bool IsGameRunning { get; set; } = true;
        static public Hero Hero { get; set; }
        public static int Score { get; set; } = 0;
        public static int PatrScore { get; set; } = 0;
        public static Texture2D Texture2D { get; set; }
        public static SoundEffect BirdDeathSound;
        public const int InitialBirdCount = 5;
        public static readonly int maxBirdsOnScreen = 15;
        private const int framesPerSecond = 180;
        private static int framesElapsed = 0;
        public static int Width, Height;
        public static Song DeathHero;
        static public List<Fire> Bullets = new();
        public static List<Bird> Birds = new();

        

        public static int GetIntRnd(int min, int max)
        {
            return rnd.Next(min, max);
        }

        static public void LoadContent(ContentManager Content)
        {
            BirdDeathSound = Content.Load<SoundEffect>("birdkill");
            DeathHero = Content.Load<Song>("deathHero");
        }

        static public void HeroFire()
        {
            Vector2 fireDirection = new((float)Math.Cos(Hero.Rotation), (float)Math.Sin(Hero.Rotation));
            Vector2 weaponPosition = Hero.GetWeaponPosition(); // Получаем позицию точки оружия
            Bullets.Add(new Fire(weaponPosition, fireDirection));
            PatrScore--;
        }        

        public static void Init(SpriteBatch spriteBatch, int width, int height)
        {
            Main.Width = width;
            Main.Height = height;
            Main.SpriteBatch = spriteBatch;
            Hero = new Hero(new Vector2(0, Height / 2 - 20));
            Birds = new List<Bird>();
        }

        public static void Draw()
        {
            foreach (Fire fire in Bullets)
                fire.Draw();
            Hero.Draw();
            foreach (Bird bird in Birds)
                bird.Draw();
            foreach (Patrons coin in Patrons.Patron)
                coin.Draw(); 
        }

        public static void Update(GameTime gameTime)
        {
            // генерация пуль
            for(int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update();
                if (Bullets[i].Hidden)
                {
                    Bullets.RemoveAt(i);
                    i--;
                }
            }
            // конец генерации пуль

            // генерация птиц 
            framesElapsed++;
            if (framesElapsed >= framesPerSecond)
            {
                int numBirds = rnd.Next(1, 4);
                for (int i = 0; i < numBirds; i++)
                {
                    Vector2 birdPos = new(Width, Height);
                    Vector2 birdDir = new(Width / 2, Height / 2);
                    Bird newBird = new(birdPos, birdDir);
                    if (Birds.Count >= maxBirdsOnScreen)
                        break;
                    Birds.Add(newBird);
                }

                framesElapsed = 0;
            }

            // столкновение пули и птицы
            HandleCollisions();

            for (int i = Birds.Count - 1; i >= 0; i--)
            {
                Birds[i].Update(gameTime);
                if (Birds[i].IsOffScreen() || Birds.Count > maxBirdsOnScreen)
                {
                    Birds.RemoveAt(i);
                }
            }
            // конец генерации птиц 


            if (!IsGameRunning)
            {
                return; // Если игра не запущена, не обновляем состояние объектов
            }

            // столкновение героя с птицей
            if (CheckHeroCollision())
            {
                MediaPlayer.Play(DeathHero);
                IsGameRunning = false; //остановка игры
                Game1.Stat = Stat.Final;
                foreach (Bird bird in Birds)
                {
                    bird.Dir = Vector2.Zero; //исчезают птица
                }
                return;
            }
            // конец проверки столкновения с птицей

            // подбор монетки
            foreach (Patrons coin in Patrons.Patron.ToList())
            {
                coin.Update();
                if (coin.IsCollected(new Rectangle((int)Hero.Pos.X, (int)Hero.Pos.Y, Hero.Texture2D.Width, Hero.Texture2D.Height)))
                {
                    Patrons.Patron.Remove(coin);
                    PatrScore += 10; // Увеличение счетчика при подборе монетки
                    
                }
            }
        }


        // проверка столкновения с птицей
        public static bool CheckHeroCollision()
        {
            float heroHalfWidth = Hero.Texture2D.Width / 2;
            float heroHalfHeight = Hero.Texture2D.Height / 2;

            Rectangle heroRect = new((int)(Hero.Pos.X - heroHalfWidth), (int)(Hero.Pos.Y - heroHalfHeight), Hero.Texture2D.Width, Hero.Texture2D.Height);

            foreach (Bird bird in Birds)
            {
                float birdHalfWidth = Bird.BirdWidth / 2;
                float birdHalfHeight = Bird.BirdWidth / 2;

                Rectangle birdRect = new((int)(bird.Pos.X - birdHalfWidth), (int)(bird.Pos.Y - birdHalfHeight), Bird.BirdWidth, Bird.BirdWidth);
                if (heroRect.Intersects(birdRect))
                {
                    return true;
                }
            }
            return false;
        }

        //столкновении пули с птицей, так же появление монетки
        public static void HandleCollisions()
        { 
            for (int i = 0; i < Bullets.Count; i++)
            {
                for (int j = 0; j < Birds.Count; j++)
                {
                    if (Bullets[i].IsCollidingWith(Birds[j]))
                    {
                        Bullets.RemoveAt(i); // исчезла пуля
                        Birds[j].Health--; // уменьшаем жизни птицы

                        if (Birds[j].Health <= 0)
                        {
                            BirdDeathSound.Play();
                            Patrons.CreatePatr(Birds[j].Pos); // появляется монетка
                            Birds.RemoveAt(j); // исчезает птица
                            Score += 10;
                        }                        
                        i--;
                        break;
                    }
                }
            }
        }   
    }
}

