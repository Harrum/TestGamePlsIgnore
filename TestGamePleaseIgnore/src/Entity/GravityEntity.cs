using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src.Entity
{
    public class GravityEntity : DynamicEntity
    {
        private const float GRAVITY = 0.05f;

        protected float Weight;
        protected bool IsAirborne;

        public GravityEntity(float x, float y, float width, float height) : base(x, y, width, height)
        {
            Weight = 1f;
            IsAirborne = false;
            this.SetHitbox(new RectangleF(1, 0, Width - 2, Height));
        }

        protected override void CheckCollision(BaseEntity col)
        {
            if (col == null)
                IsAirborne = true;
            else
            {
                if ((this.Hitbox.Left >= col.Hitbox.Left && this.Hitbox.Left <= col.Hitbox.Right) ||
                    (this.Hitbox.Right <= col.Hitbox.Right && this.Hitbox.Right >= col.Hitbox.Left))
                {
                    if (this.Hitbox.Bottom >= col.Hitbox.Top && this.Hitbox.Bottom <= col.Hitbox.Bottom)
                    {
                        IsAirborne = false;
                        SpeedY = 0;
                        this.Y = col.Hitbox.Top - this.Hitbox.Height;
                        //col.drawHitbox = true;
                    }
                }
                else
                {
                    IsAirborne = true;
                }
            }
        }

        public override void Update(long elapsedTime)
        {
            if(IsAirborne)
            {
                this.SpeedY += GRAVITY * Weight;
            }
            base.Update(elapsedTime);
        }
    }
}
