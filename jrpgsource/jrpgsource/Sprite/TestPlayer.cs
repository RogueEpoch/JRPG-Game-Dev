using jrpgsource.SpriteManager;
using jrpgsource.SpriteManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jrpgsource.Sprite
{
    public class TestPlayer : AnimatedSprite
    {
        private int speed = 3; // Player speed
        public TestPlayer(String name, Texture2D texture, int frameCount, int framesPerSec)
            : base(name, texture, frameCount, framesPerSec)
        {
        }

        public sealed override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Set the bounds to the window width/height.
            Vector2 bounds = new Vector2(
                ((Game1)SpriteManager.Game).Graphics.PreferredBackBufferWidth,
                ((Game1)SpriteManager.Game).Graphics.PreferredBackBufferHeight);

            // Move the player with WASD.
            KeyboardState key1 = Keyboard.GetState();
            if (key1.IsKeyDown(Keys.D))
            {
                // Note: We must use Rect.Width here rather than Texture.Width, 
                // otherwise it will use the whole width of the sprite sheet.
                if (Position.X <= bounds.X - Rect.Width)
                    Position.X += speed;
            }
            else if (key1.IsKeyDown(Keys.A))
            {
                if (Position.X >= 0)
                    Position.X -= speed;
            }
            else if (key1.IsKeyDown(Keys.W))
            {
                if (Position.Y >= 0)
                    Position.Y -= speed;
            }
            else if (key1.IsKeyDown(Keys.S))
            {
                if (Position.Y <= bounds.Y - Rect.Height)
                    Position.Y += speed;
            }
        }
    }
}
