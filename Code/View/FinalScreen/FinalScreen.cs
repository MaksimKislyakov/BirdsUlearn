using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Birds.Code.View.SplashScreen;

namespace Birds.Code.View.FinalScreen
{
    class FinalScreen
    {
        public static Texture2D Background { get; set; }
        static Color color;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new(1000, 200);

        public static Texture2D backButtonTexture;
        public static Rectangle backButtonRect;

        static public void LoadContent(ContentManager Content)
        {
            backButtonTexture = Content.Load<Texture2D>("ButtonBackSplashSceen");
            Background = Content.Load<Texture2D>("finalbackground");

            int buttonWidth = backButtonTexture.Width;
            int buttonHeight = backButtonTexture.Height;
            int screenWidth = 922;
            int screenHeight = 450;

            backButtonRect = new Rectangle(screenWidth, screenHeight, buttonWidth, buttonHeight);
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 backgroundPos = new(0, 0);
            spriteBatch.Draw(Background, backgroundPos, Color.White);
            spriteBatch.DrawString(SplashScreen.SplashScreen.Font, "Game over", textPosition, Color.Black);
            spriteBatch.DrawString(SplashScreen.SplashScreen.Font, "Ваш счет: " + Main.Score, new Vector2(1000, 300), Color.Black);
            spriteBatch.DrawString(SplashScreen.SplashScreen.Font, "Патроны: " + Main.PatrScore, new Vector2(1000, 350), Color.Black);
            spriteBatch.Draw(backButtonTexture, backButtonRect, Color.White);
        }

        static public void Update()
        {
            color = Color.FromNonPremultiplied(0, 0, 0, 256);
        }
    }
}
