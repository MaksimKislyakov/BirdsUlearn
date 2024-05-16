using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Birds.Code
{
    class GameScreen
    {
        public static Texture2D Background { get; set; }
        static Color color;
        public static SpriteFont Font { get; set; }
        static Vector2 textPosition = new(10 , 10);
        public static Song gameSong;

        static public void LoadContent(ContentManager Content)
        {
            Background = Content.Load<Texture2D>("gamebackground");
            Font = Content.Load<SpriteFont>("splashfont");
            gameSong = Content.Load<Song>("gamesong");
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 backgroundPos = new(0, 0);
            spriteBatch.Draw(Background, backgroundPos, Color.White);
            spriteBatch.DrawString(Font, "Счет: " + Main.Score, textPosition, color);
            spriteBatch.DrawString(Font, "Патроны: " + Main.PatrScore, new Vector2(10, 70), color);
        }

        static public void Update()
        {
            color = Color.FromNonPremultiplied(0, 0, 0, 256);
        }
    }
}
