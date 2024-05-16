using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Birds.Code
{
    class InformationScreen
    {
        public static Texture2D Background { get; set; }
        static Color color;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new(1000, 200);

        public static Texture2D backButtonTexture;
        public static Rectangle backButtonRect;

        static public void LoadContent(ContentManager Content)
        {
            Background = Content.Load<Texture2D>("Informationback");
            backButtonTexture = Content.Load<Texture2D>("ButtonBackSplashSceen");

            int buttonWidth = backButtonTexture.Width;
            int buttonHeight = backButtonTexture.Height;
            int buttonX = 1100;
            int buttonY = 830; 

            backButtonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 backgroundPos = new(0, 0);
            spriteBatch.Draw(Background, backgroundPos, Color.White);
            spriteBatch.Draw(backButtonTexture, backButtonRect, Color.White);
        }
    }
}
