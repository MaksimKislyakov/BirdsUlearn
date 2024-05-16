using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Birds.Code
{
    internal class Patrons
    {
        public Vector2 Pos { get; set; }
        public static List<Patrons> Patron = new();
        public static Texture2D Texture2D { get; set; }
        bool isCollected = false;
        public static SoundEffect CoinSound;

        public Patrons(Vector2 pos)
        {
            this.Pos = pos;
        }

        static public void LoadContent(ContentManager Content)
        {
            Texture2D = Content.Load<Texture2D>("patron");
            CoinSound = Content.Load<SoundEffect>("patrsound");
        }

        public static void CreatePatr(Vector2 position)
        {
            if (Main.rnd.NextDouble() <= 0.7) // 30% шанс появления монетки
            {
                Patrons newPatr = new(position);
                Patron.Add(newPatr);
            }
        }
        public bool IsCollected(Rectangle playerRect)
        {
            Rectangle patronRect = new Rectangle((int)Pos.X, (int)Pos.Y, Texture2D.Width, Texture2D.Height);

            if (!isCollected && playerRect.Intersects(patronRect))
            {
                CoinSound.Play();
                isCollected = true;
            }
            return isCollected;
        }

        public void Update()
        {
            return;
        }

        public void Draw()
        {
            if (!isCollected)
            {
                Main.SpriteBatch.Draw(Texture2D, Pos, Color.White);
            }
        }
    }
}
