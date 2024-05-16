using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace Birds.Code.Controller.CreateBird
{
    internal class Bird
    {
        public Vector2 Pos { get; private set; }
        private Vector2 dir;
        static Color color;


        public static Texture2D Texture2DFir { get; set; }
        public static Texture2D Texture2DSec { get; set; }
        public static Texture2D Texture2DThi { get; set; }
        public static Texture2D Texture2DFou { get; set; }
        public static Texture2D Texture2DFif { get; set; }
        public static Texture2D Texture2DSix { get; set; }
        public static Texture2D Texture2DSev { get; set; }
        public static Texture2D Texture2DEig { get; set; }
        public static SpriteFont Font { get; set; }
        public int Health { get; set; }

        public Vector2 Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public static int BirdWidth => Texture2DFir.Width;

        public bool IsOffScreen()
        {
            float birdHalfWidth = Texture2DFir.Width / 2;
            float birdHalfHeight = Texture2DFir.Height / 2;

            return Pos.X + birdHalfWidth < 0 || Pos.Y + birdHalfHeight < 0 ||
                   Pos.X - birdHalfWidth > Main.Width || Pos.Y - birdHalfHeight > Main.Height;
        }

        static public void LoadContent(ContentManager Content)
        {
            Texture2DFir = Content.Load<Texture2D>("bird1");
            Texture2DSec = Content.Load<Texture2D>("bird2");
            Texture2DThi = Content.Load<Texture2D>("bird3");
            Texture2DFou = Content.Load<Texture2D>("bird4");
            Texture2DFif = Content.Load<Texture2D>("bird5");
            Texture2DSix = Content.Load<Texture2D>("bird6");
            Texture2DSev = Content.Load<Texture2D>("bird7");
            Texture2DEig = Content.Load<Texture2D>("bird8");
            Font = Content.Load<SpriteFont>("splashfont");
        }

        public static int BirdHealth()
        {
            var health = 0;
            if (Main.Score >= 0 && Main.Score < 300) health = 1;
            if (Main.Score >= 300 && Main.Score < 600) health = 2;
            if (Main.Score >= 600 && Main.Score < 900) health = 3;
            if (Main.Score >= 900 && Main.Score < 1200) health = 4;
            if (Main.Score >= 1200 && Main.Score < 1500) health = 5;
            if (Main.Score >= 1500 && Main.Score < 1800) health = 6;
            if (Main.Score >= 1800 && Main.Score < 2100) health = 7;
            if (Main.Score >= 2100) health = 8;
            return health;
        }


        public Bird(Vector2 pos, Vector2 dir)
        {
            Pos = pos;
            Dir = dir;
            Health = BirdHealth();
        }

        public void Update(GameTime gameTime)
        {
            Pos += Dir;
            if (Pos.X < 0 || Pos.Y < 0 || Pos.X > Main.Width || Pos.Y > Main.Height)
            {
                RandomSet();
            }

            color = Color.FromNonPremultiplied(0, 0, 0, 256);
        }

        public void RandomSet()
        {
            Random rnd = new();
            int side = rnd.Next(4);
            int posX = 0, posY = 0;
            int angleDegrees = rnd.Next(30); // Угол траектории в градусах
            double angleRadians = MathHelper.ToRadians(angleDegrees); // Преобразование в радианы
            Dir = new Vector2((float)Math.Cos(angleRadians), (float)Math.Sin(angleRadians));
            Dir.Normalize();
            switch (side)
            {
                case 0: // Слева
                    posX = 0;
                    posY = rnd.Next(Main.Height);
                    Dir = new Vector2(Main.GetIntRnd(1, 4), (float)Math.Sin(angleRadians));
                    //Dir = new Vector2(Main.GetIntRnd(1, 4), 0); // Вправо
                    break;
                case 1: // Сверху
                    posX = rnd.Next(Main.Width);
                    posY = 0;
                    Dir = new Vector2((float)Math.Cos(angleRadians), Main.GetIntRnd(1, 4));
                    //Dir = new Vector2(0, Main.GetIntRnd(1, 4)); // Вниз
                    break;
                case 2: // Справа
                    posX = Main.Width - BirdWidth;
                    posY = rnd.Next(Main.Height);
                    Dir = new Vector2(-Main.GetIntRnd(1, 4), (float)Math.Sin(angleRadians));
                    //Dir = new Vector2(-Main.GetIntRnd(1, 4), 0); // Влево
                    break;
                case 3: // Снизу
                    posX = rnd.Next(Main.Width - BirdWidth);
                    posY = Main.Height - BirdWidth;
                    Dir = new Vector2((float)Math.Cos(angleRadians), -Main.GetIntRnd(1, 4));
                    //Dir = new Vector2(0, -Main.GetIntRnd(1, 4)); // Вверх
                    break;
            }
            Pos = new Vector2(posX, posY);
        }

        public void Draw()
        {
            float rotation;
            Vector2 origin = new(Texture2DFir.Width / 2, Texture2DFir.Height / 2);

            if (Math.Abs(Dir.X) > Math.Abs(Dir.Y))
            {
                if (Dir.X > 0)
                    rotation = MathHelper.Pi / 2 * 3; // вправо
                else
                    rotation = MathHelper.Pi / 2; // влево
            }
            else
            {
                // Движение по вертикали
                if (Dir.Y > 0)
                    rotation = MathHelper.Pi * 2; // вниз
                else
                    rotation = MathHelper.Pi; // вверх
            }

            //Main.SpriteBatch.Draw(Texture2D, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 0 && Main.Score < 300)
                Main.SpriteBatch.Draw(Texture2DFir, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 300 && Main.Score < 600)
                Main.SpriteBatch.Draw(Texture2DSec, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 600 && Main.Score < 900)
                Main.SpriteBatch.Draw(Texture2DThi, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 900 && Main.Score < 1200)
                Main.SpriteBatch.Draw(Texture2DFou, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 1200 && Main.Score < 1500)
                Main.SpriteBatch.Draw(Texture2DFif, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 1500 && Main.Score < 1800)
                Main.SpriteBatch.Draw(Texture2DSix, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 1800 && Main.Score < 2100)
                Main.SpriteBatch.Draw(Texture2DSev, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            if (Main.Score >= 2100)
                Main.SpriteBatch.Draw(Texture2DEig, Pos, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
