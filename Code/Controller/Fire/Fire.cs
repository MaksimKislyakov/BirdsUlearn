using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Birds.Code.Controller.CreateBird;

namespace Birds.Code
{
    internal class Fire
    {
        Vector2 Pos;
        Vector2 Dir;
        const int speed = 10;
        Color color = Color.White;
        public static SoundEffect ShootSound;

        public static Texture2D Texture2DFir { get; set; }
        public static Texture2D Texture2DSec { get; set; }
        public static Texture2D Texture2DThi { get; set; }
        public static Texture2D Texture2DFou { get; set; }
        public static Texture2D Texture2DFif { get; set; }
        public static Texture2D Texture2DSix { get; set; }
        public static Texture2D Texture2DSev { get; set; }
        public static Texture2D Texture2DEig { get; set; }

        static public List<Fire> Bullets = new();
       

        public Fire(Vector2 weaponPosition, Vector2 direction)
        {
            this.Pos = weaponPosition;
            this.Dir = Vector2.Normalize(direction) * speed;
        }

        public bool IsCollidingWith(Bird bird)
        {
            Rectangle fireRect = new((int)Pos.X, (int)Pos.Y, Texture2DFir.Width, Texture2DFir.Height);
            Rectangle birdRect = new((int)bird.Pos.X, (int)bird.Pos.Y, Bird.BirdWidth, Bird.BirdWidth);
            return fireRect.Intersects(birdRect);
        }

        public bool Hidden
        {
            get
            {
                return Pos.X > Main.Width;
            }
        }

        static public void LoadContent(ContentManager Content)
        {
            Texture2DFir = Content.Load<Texture2D>("bullet1");
            Texture2DSec = Content.Load<Texture2D>("bullet2");
            Texture2DThi = Content.Load<Texture2D>("bullet3");
            Texture2DFou = Content.Load<Texture2D>("bullet4");
            Texture2DFif = Content.Load<Texture2D>("bullet5");
            Texture2DSix = Content.Load<Texture2D>("bullet6");
            Texture2DSev = Content.Load<Texture2D>("bullet7");
            Texture2DEig = Content.Load<Texture2D>("bullet8");
            ShootSound = Content.Load<SoundEffect>("firesound");
        }

        public void Update()
        {
            Pos += Dir;
            if (Pos.X < Main.Width)
            {
                Pos += Dir;
            }
        }

        public void Draw()
        {
            Rectangle destinationRect = new((int)Pos.X, (int)Pos.Y, Texture2DFir.Width, Texture2DFir.Height);

            if (Main.PatrScore >= 0 && Main.PatrScore < 100)
                Main.SpriteBatch.Draw(Texture2DFir, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 100 && Main.PatrScore < 200)
                Main.SpriteBatch.Draw(Texture2DSec, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 200 && Main.PatrScore < 300)
                Main.SpriteBatch.Draw(Texture2DThi, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 300 && Main.PatrScore < 400)
                Main.SpriteBatch.Draw(Texture2DFou, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 400 && Main.PatrScore < 500)
                Main.SpriteBatch.Draw(Texture2DFif, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 500 && Main.PatrScore < 600)
                Main.SpriteBatch.Draw(Texture2DSix, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 600 && Main.PatrScore < 700)
                Main.SpriteBatch.Draw(Texture2DSev, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            if (Main.PatrScore >= 700)
                Main.SpriteBatch.Draw(Texture2DEig, destinationRect, null, color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
