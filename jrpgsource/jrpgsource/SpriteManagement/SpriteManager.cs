﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jrpgsource.SpriteManagement
{
    public class SpriteManager : DrawableGameComponent
    {
        #region Fields

        private SortedDictionary<string, Sprite> sprites =
            new SortedDictionary<string, Sprite>();
        private SpriteBatch spriteBatch;
        private float cameraPositionX = 0.0f;
        private float cameraPositionY = 0.0f;
        private bool manualDraw = false;
        private bool manualUpdate = false;

        #endregion


        #region Properties

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }
        public float CameraPositionX
        {
            get { return cameraPositionX; }
            set { cameraPositionX = value; }
        }
        public float CameraPositionY
        {
            get { return cameraPositionY; }
            set { cameraPositionY = value; }
        }
        public bool ManualDraw
        {
            get { return manualDraw; }
            set { manualDraw = value; }
        }
        public bool ManualUpdate
        {
            get { return manualUpdate; }
            set { manualUpdate = value; }
        }

        #endregion


        #region Initialization

        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        #endregion


        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!manualUpdate)
                UpdateSprites(gameTime);
        }

        public void UpdateSprites(GameTime gameTime)
        {
            foreach (var pair in sprites)
            {
                Sprite sprite = (Sprite)pair.Value;
                sprite.Update(gameTime);

                // Remove the sprite if it is to be destroyed this frame.
                if (sprite.DestroyNextFrame)
                {
                    DestroySprite(sprite.Name);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (!manualDraw)
            {
                Matrix cameraTransform = Matrix.CreateTranslation(-CameraPositionX, -CameraPositionY, 0.0f);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null,
                    cameraTransform);

                DrawSprites(gameTime);
                DrawSpritesLate(gameTime);

                spriteBatch.End();
            }
        }

        public void DrawSprites(GameTime gameTime)
        {
            foreach (var pair in sprites)
            {
                Sprite sprite = (Sprite)pair.Value;
                if (!sprite.DrawLate)
                    sprite.Draw(gameTime, spriteBatch);
            }
        }

        public void DrawSpritesLate(GameTime gameTime)
        {
            foreach (var pair in sprites)
            {
                Sprite sprite = (Sprite)pair.Value;
                if (sprite.DrawLate)
                    sprite.Draw(gameTime, spriteBatch);
            }
        }

        #endregion


        #region Disposal

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (spriteBatch != null)
                {
                    spriteBatch.Dispose();
                    spriteBatch = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion


        #region Sprite Management

        /// <summary>
        /// Creates a sprite by object.
        /// </summary>
        /// <param name="sprite">The sprite object to create</param>
        public Sprite CreateSprite(Sprite sprite)
        {
            if (!HasSprite(sprite.Name))
            {
                sprites.Add(sprite.Name, sprite);
                sprite.SpriteManager = this;
                sprite.Initialize();
                return sprite;
            }
            return null;
        }

        /// <summary>
        /// Creates a sprite by object and position.
        /// </summary>
        /// <param name="sprite">The sprite object to create</param>
        /// <param name="position">The position of the sprite to create</param>
        public Sprite CreateSprite(Sprite sprite, Vector2 position)
        {
            sprite.Position = position;
            return CreateSprite(sprite);
        }

        /// <summary>
        /// Creates a sprite by name and texture.
        /// </summary>
        /// <param name="name">The name of the sprite to create</param>
        /// <param name="texture">The texture of the sprite to create</param>
        public Sprite CreateSprite(String name, Texture2D texture)
        {
            return CreateSprite(new Sprite(name, texture));
        }

        /// <summary>
        /// Creates a sprite by name, texture and position.
        /// </summary>
        /// <param name="name">The name of the sprite to create</param>
        /// <param name="texture">The texture of the sprite to create</param>
        /// <param name="position">The position of the sprite to create</param>
        public Sprite CreateSprite(String name, Texture2D texture, Vector2 position)
        {
            Sprite sprite = new Sprite(name, texture);
            sprite.Position = position;
            return CreateSprite(sprite);
        }

        /// <summary>
        /// Gets a sprite by name.
        /// </summary>
        /// <param name="name">Name of the sprite to get</param>
        public Sprite GetSprite(String name)
        {
            if (HasSprite(name))
            {
                return sprites[name];
            }
            return null;
        }

        /// <summary>
        /// Gets a list of sprites by tag.
        /// </summary>
        /// <param name="tag">Tag of the sprites to get</param>
        public List<Sprite> GetSpritesWithTag(String tag)
        {
            List<Sprite> tagSprites = new List<Sprite>();
            foreach (var pair in sprites)
            {
                Sprite sprite = (Sprite)pair.Value;
                if (sprite.Tag == tag)
                    tagSprites.Add(sprite);
            }
            return tagSprites;
        }

        /// <summary>
        /// Gets a sprite by tag.
        /// </summary>
        /// <param name="tag">Tag of the sprite to get</param>
        public Sprite GetSpriteWithTag(String tag)
        {
            List<Sprite> tagSprites = new List<Sprite>();
            foreach (var pair in sprites)
            {
                Sprite sprite = (Sprite)pair.Value;
                if (sprite.Tag == tag)
                    return sprite;
            }
            return null;
        }

        /// <summary>
        /// Whether the SpriteManager has the specified sprite by name.
        /// </summary>
        /// <param name="name">Name of the sprite to check</param>
        public bool HasSprite(String name)
        {
            return sprites.ContainsKey(name);
        }

        /// <summary>
        /// Destroys a sprite by name.
        /// </summary>
        /// <param name="name">Name of the sprite to destroy.</param>
        public void DestroySprite(String name)
        {
            sprites.Remove(name);
        }

        /// <summary>
        /// Destroys all sprites. Note that here we use our RemoveAll extension method.
        /// </summary>
        public void DestroyAllSprites()
        {
            sprites.RemoveAll(x => true);
        }

        #endregion
    }
}
