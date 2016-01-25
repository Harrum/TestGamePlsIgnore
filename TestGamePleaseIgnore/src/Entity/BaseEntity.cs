using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.game;
using TestGamePleaseIgnore.src.Texture;

namespace TestGamePleaseIgnore.src.Entity
{
    public class BaseEntity
    {
        public enum Direction
        {
            Left, Right, Up, Down
        }

        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float Width { get; protected set; }
        public float Height { get; protected set; }
        public RectangleF Hitbox { get; private set; }
        public int Layer { get; protected set; }

        public bool Mirrored { get; protected set; }

        private BaseTexture Texture;

        private RectangleF DrawingPositionRect;
        private RectangleF HitboxBounds;

        //public bool drawHitbox = false;

        public BaseEntity() : this(0, 0, 0, 0)
        {
        }

        public BaseEntity(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            SetHitbox(new RectangleF(0, 0, Width, Height));
            this.Mirrored = false;
            this.Layer = 1;
            this.Texture = null;
        }

        public virtual void HandleCollisions(List<BaseEntity> collisions)
        {
            if (collisions.Count > 0)
            {
                foreach (BaseEntity col in collisions)
                {
                    CheckCollision(col);
                }
            }
            else    //No collisions but still handy to let the object know it has none.
            {
                CheckCollision(null);
            } 
        }

        protected virtual void CheckCollision(BaseEntity col)
        {

        }

        protected void SetTexture(BaseTexture texture)
        {
            if(texture != null)
            {
                this.Texture = texture;
            }
        }

        protected void SetHitbox(RectangleF hitbox)
        {
            this.HitboxBounds = hitbox;
            UpdateHitbox();
        }

        protected void UpdateHitbox()
        {
            this.Hitbox = new RectangleF(X + HitboxBounds.X, Y + HitboxBounds.Y, HitboxBounds.Width, HitboxBounds.Height);
        }

        public virtual void Update(long elapsedTime)
        {
            if(Texture != null)
            {
                if(Texture.GetType() ==  typeof(AnimatedTexture))
                {
                    Texture.Update(elapsedTime);
                }
            }
            //drawHitbox = false;
        }

        public virtual void Draw(RenderTarget g)
        {
            //Texture draw call
            if (Texture != null)
            {
                DrawingPositionRect = new RectangleF(X - Game.VIEWPORT.X, Y - Game.VIEWPORT.Y, Width, Height);
                Texture.DrawTexture(g, new RectangleF(X, Y, Width, Height));
            }           

            //Debug drawing options
            if (Config.DEBUG_MODE == DebugMode.DISPLAY_HITBOX)
            {
                g.DrawRectangle(new RectangleF(Hitbox.X, Hitbox.Y, Hitbox.Width, Hitbox.Height), Resources.SCBRUSH_RED);
            }
            else if (Config.DEBUG_MODE == DebugMode.DISPLAY_RECT)
            {
                g.DrawRectangle(new RectangleF(X, Y, Width, Height), Resources.SCBRUSH_RED);
                g.DrawLine(new Vector2(X, Y), new Vector2(X + Width, Y + Height), Resources.SCBRUSH_RED);
                g.DrawLine(new Vector2(X, Y + Height), new Vector2(X + Width, Y), Resources.SCBRUSH_RED);
            }
            /* Used for debug purposes
            if (drawHitbox)
                g.DrawRectangle(Hitbox, Resources.SCBRUSH_BLACK, 3f);
            */
        }
    }
}
