using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src.Entity
{
    public class BaseEntity
    {
        public float X { get; protected set; }
        public float Y { get; protected set; }
        public float Width { get; protected set; }
        public float Height { get; protected set; }
        public RectangleF Hitbox { get; private set; }

        public bool Mirrored { get; protected set; }

        private RectangleF DrawingPositionRect;
        private RectangleF HitboxBounds;

        public BaseEntity(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            SetHitbox(new RectangleF(0, 0, Width, Height));
            this.Mirrored = false;
        }

        protected void SetHitbox(RectangleF hitbox)
        {
            this.HitboxBounds = hitbox;
            UpdateHitbox();
        }

        private void UpdateHitbox()
        {
            this.Hitbox = new RectangleF(X + HitboxBounds.X, Y + HitboxBounds.Y, HitboxBounds.Width, HitboxBounds.Height);
        }

        public void Update(long gameTime)
        {
            UpdateHitbox();
        }

        public void Draw(RenderTarget g)
        {
            g.DrawRectangle(new RectangleF(X, Y, Width, Height), Resources.SCBRUSH_RED);
            g.DrawLine(new Vector2(X, Y), new Vector2(X + Width, Y + Height), Resources.SCBRUSH_RED);
        }
    }
}
