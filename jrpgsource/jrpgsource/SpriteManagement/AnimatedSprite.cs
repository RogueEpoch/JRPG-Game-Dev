using jrpgsource.SpriteManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jrpgsource.SpriteManagement
{
    public class AnimatedSprite : Sprite
    {
        #region Fields

        protected int frameCount;
        protected int frame = 0;
        protected int frameWidth = 0;

        private float timePerFrame;
        private bool paused = false;
        private float timeElapsed;

        #endregion


        #region Properties

        public int FrameWidth
        {
            get { return frameWidth; }
        }
        public bool IsPaused
        {
            get { return paused; }
        }
        public override Rectangle Rect
        {
            get
            {
                if (rectangle == Rectangle.Empty)
                    return new Rectangle(0, 0, FrameWidth, Texture.Height);

                return rectangle;
            }
            set { rectangle = value; }
        }

        #endregion


        #region Initialisation

        public AnimatedSprite(String name, Texture2D texture, int frameCount, int framesPerSec)
            : base(name, texture)
        {
            this.frameCount = frameCount;
            this.timePerFrame = 1f / framesPerSec;
        }

        public AnimatedSprite(String name, Texture2D texture, Vector2 position,
            Color colour, Vector2 origin, float rotation, Vector2 scale, float depth,
            int frameCount, int framesPerSec)
            : base(name, texture, position, colour, origin, rotation, scale, depth)
        {
            this.frameCount = frameCount;
            this.timePerFrame = 1f / framesPerSec;
        }

        public override void Initialize() { }

        #endregion


        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            if (!paused)
            {
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeElapsed > timePerFrame)
                {
                    frame++;
                    frame = frame % frameCount;
                    timeElapsed -= timePerFrame;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                frameWidth = Texture.Width / frameCount;
                Rectangle sourceRect = new Rectangle(frameWidth * frame, 0,
                    frameWidth, Texture.Height);
                spriteBatch.Draw(Texture, Position, sourceRect, Colour,
                    Rotation, Origin, Scale, SpriteEffects, Depth);
            }
        }

        #endregion


        #region Animation Control

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public void Reset()
        {
            frame = 0;
            timeElapsed = 0f;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            Pause();
            Reset();
        }

        /// <summary>
        /// Plays the animation.
        /// </summary>
        public void Play()
        {
            paused = false;
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            paused = true;
        }

        #endregion
    }
}
