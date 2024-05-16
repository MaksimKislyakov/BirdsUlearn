using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Birds.Code.View.SplashScreen
{
    public class SplashScreen
    {
        public static Texture2D Background { get; set; }
        static Color color;
        public static SoundEffect MenuClick;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new(500, 100);
        public static Song menuSong;
        public static Texture2D startButtonTexture;
        public static Texture2D exitButtonTexture;
        public static Texture2D infoButtonTexture;
        public static Rectangle startButtonRect;
        public static Rectangle exitButtonRect;
        public static Rectangle infoButtonRect;

        static public void LoadContent(ContentManager Content)
        {
            startButtonTexture = Content.Load<Texture2D>("buttonstartgame");
            exitButtonTexture = Content.Load<Texture2D>("buttonexitgame");
            infoButtonTexture = Content.Load<Texture2D>("information");
            Background = Content.Load<Texture2D>("background");
            Font = Content.Load<SpriteFont>("splashfont");
            MenuClick = Content.Load<SoundEffect>("menuclick");
            menuSong = Content.Load<Song>("menusong");

            int buttonWidth = startButtonTexture.Width;
            int buttonHeight = startButtonTexture.Height;
            int screenWidth = 1600;
            int screenHeight = 550;

            startButtonRect = new Rectangle(screenWidth / 2 - buttonWidth / 2, screenHeight / 2 - buttonHeight, buttonWidth, buttonHeight);
            exitButtonRect = new Rectangle(screenWidth / 2 - buttonWidth / 2 + 20, screenHeight / 2 + buttonHeight * 2, buttonWidth, buttonHeight);
            infoButtonRect = new Rectangle(screenWidth / 2 - buttonWidth / 2, screenHeight / 2 + buttonHeight / 2, buttonWidth, buttonHeight);

        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 backgroundPos = new(0, -500);
            spriteBatch.Draw(Background, backgroundPos, Color.White);
            spriteBatch.DrawString(Font, "Птичья лихорадка", textPosition, color);
            spriteBatch.Draw(startButtonTexture, startButtonRect, Color.White);
            spriteBatch.Draw(exitButtonTexture, exitButtonRect, Color.White);
            spriteBatch.Draw(infoButtonTexture, infoButtonRect, Color.White);

        }

        static public void Update()
        {
            color = Color.FromNonPremultiplied(0, 0, 0, 255);
        }
    }
}
