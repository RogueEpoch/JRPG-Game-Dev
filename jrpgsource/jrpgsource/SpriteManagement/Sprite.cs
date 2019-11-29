using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jrpgsource.SpriteManagement
{
    public class Sprite
    {
        #region Fields

        protected String name;
        protected String tag;
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        protected Rectangle rectangle = Rectangle.Empty;
        protected Vector2 velocity = Vector2.Zero;
        protected float rotationVelocity = 0.0f;
        protected float rotation = 0.0f;
        protected float depth = 0.5f;
        protected Vector2 scale = Vector2.One;
        protected Color color = Color.White;
        protected bool visible = true;
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected Texture2D texture;
        protected SpriteManager spriteManager;
        protected bool destroyNextFrame = false;
        protected bool drawLate = false;

        #endregion


        #region Properties

        public String Name
        {
            get { return name; }
        }
        public String Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public float RotationVelocity
        {
            get { return rotationVelocity; }
            set { rotationVelocity = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Color Colour
        {
            get { return color; }
            set { color = value; }
        }
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public SpriteEffects SpriteEffects
        {
            get { return spriteEffects; }
            set { spriteEffects = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public SpriteManager SpriteManager
        {
            get { return spriteManager; }
            internal set { spriteManager = value; }
        }
        public virtual Rectangle Rect
        {
            get
            {
                if (rectangle == Rectangle.Empty)
                    return new Rectangle(0, 0, Texture.Width, Texture.Height);

                return rectangle;
            }
            set { rectangle = value; }
        }
        public bool DestroyNextFrame
        {
            get { return destroyNextFrame; }
            set { destroyNextFrame = value; }
        }
        public bool DrawLate
        {
            get { return drawLate; }
            set { drawLate = value; }
        }

        #endregion


        #region Initialisation

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="texture">Texture of the sprite</param>
        public Sprite(String name, Texture2D texture)
        {
            this.name = name;
            this.tag = "default";
            this.texture = texture;
        }

        /// <summary>
        /// Parameter constructor
        /// </summary>
        /// <param name="texture">Texture of the sprite</param>
        /// <param name="position">Position of the sprite</param>
        /// <param name="origin">Origin of the sprite</param>
        /// <param name="rotation">Rotation of the sprite</param>
        /// <param name="scale">Scale of the sprite</param>
        /// <param name="depth">Depth of the sprite</param>
        public Sprite(String name, Texture2D texture, Vector2 position,
            Color colour, Vector2 origin, float rotation, Vector2 scale, float depth)
        {
            this.name = name;
            this.tag = "default";
            Position = position;
            this.color = colour;
            Origin = origin;
            this.texture = texture;
            this.rotation = rotation;
            this.scale = scale;
            this.depth = depth;
        }

        /// <summary>
        /// Initialises the sprite
        /// </summary>
        public virtual void Initialize() { }

        #endregion


        #region Collision

        /// <summary>
        /// A boilerplate collision function for testing rectangle intersection.
        /// </summary>
        /// <param name="other">The sprite to test against</param>
        public virtual bool CollidesWith(Sprite other)
        {
            return (new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height)).Intersects(
                new Rectangle((int)other.Position.X, (int)other.Position.Y, other.Rect.Width, other.Rect.Height));
        }

        #endregion


        #region Update and Draw

        /// <summary>
        /// Updates the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        public virtual void Update(GameTime gameTime)
        {
            Rotation += RotationVelocity;
            Position += Velocity;
        }

        /// <summary>
        /// Draws the sprite
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The sprite batch</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(Texture, Position, Rect, Colour, Rotation, Origin, Scale, SpriteEffects, Depth);
            }
        }

        #endregion
    }
}
