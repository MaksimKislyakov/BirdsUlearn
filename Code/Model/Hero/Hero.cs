using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Birds.Code.Controller.HeroMove;

namespace Birds.Code
{
    internal class Hero
    {
        public Vector2 Pos;
        Color color = Color.White;
        public int HeroSpeed { get; set; } = 7;
        public static Texture2D Texture2D { get; set; }
        public float Rotation { get; set; } = 0;
        public Vector2 Origin { get; private set; }

        public Hero(Vector2 Pos)
        {
            this.Pos = Pos;
        }

        static public void LoadContent(ContentManager Content)
        {
            Texture2D = Content.Load<Texture2D>("hero");
        }

        public void Move(MovingDirection direction)
        {
            float textureHalfWidth = Texture2D.Width / 2;
            float textureHalfHeight = Texture2D.Height / 2;

            switch (direction)
            {
                case MovingDirection.Up:
                    if (Pos.Y - textureHalfHeight > 0)
                        Pos.Y -= HeroSpeed;
                    break;
                case MovingDirection.Down:
                    if (Pos.Y + textureHalfHeight < Main.Height)
                        Pos.Y += HeroSpeed;
                    break;
                case MovingDirection.Left:
                    if (Pos.X - textureHalfWidth > 0)
                        Pos.X -= HeroSpeed;
                    break;
                case MovingDirection.Right:
                    if (Pos.X + textureHalfWidth < Main.Width)
                        Pos.X += HeroSpeed;
                    break;
                default:
                    break;
            }
        }

        public Vector2 GetWeaponPosition()
        {
            Vector2 weaponOffset = new(Texture2D.Width / 8, Texture2D.Height / 8 - 10);
            Matrix rotationMatrix = Matrix.CreateRotationZ(Rotation);
            return Pos + Vector2.Transform(weaponOffset, rotationMatrix);
        }

        public void RotateTowardsMouse(MouseState mouseState)
        {
            Vector2 mousePos = new(mouseState.X, mouseState.Y);
            Vector2 direction = Vector2.Normalize(mousePos - Pos);
            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public void Draw()
        {
            Vector2 origin = new(Texture2D.Width / 2, Texture2D.Height / 2); // чтобы крутился вокруг своей оси
            Main.SpriteBatch.Draw(Texture2D, Pos, null, color, Rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
