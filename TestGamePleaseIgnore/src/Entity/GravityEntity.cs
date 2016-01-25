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
        protected float Gravity = 0.05f;
        protected float Decelleration = 0.03f;

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
            //Collission check horizontal
            if ((Hitbox.Left + 2 > col.Hitbox.Left && Hitbox.Left + 2 < col.Hitbox.Right) ||
                (Hitbox.Right - 2 < col.Hitbox.Right && Hitbox.Right -2 > col.Hitbox.Left) ||
                (Hitbox.Center.X > col.Hitbox.Left && Hitbox.Center.X < col.Hitbox.Right))
            {
                //Collision down
                if (Hitbox.Bottom >= col.Hitbox.Top && Hitbox.Bottom <= col.Hitbox.Center.Y)
                {
                    Collision(col, Direction.Down);
                }
                //Collision up
                if(Hitbox.Top <= col.Hitbox.Bottom && Hitbox.Top >= col.Hitbox.Center.Y)
                {
                    Collision(col, Direction.Up);
                }
            }
            //Collision check vertical
            if ((Hitbox.Top > col.Hitbox.Top && Hitbox.Top < col.Hitbox.Bottom) ||
                (Hitbox.Bottom < col.Hitbox.Bottom && Hitbox.Bottom > col.Hitbox.Top) ||
                (Hitbox.Center.Y > col.Hitbox.Top && Hitbox.Center.Y < col.Hitbox.Bottom))
            {
                //Collision left
                if (Hitbox.Left <= col.Hitbox.Right && Hitbox.Left >= col.Hitbox.Center.X)
                {
                    Collision(col, Direction.Left);
                }
                //Collision right
                else if (Hitbox.Right >= col.Hitbox.X && Hitbox.Right <= col.Hitbox.Center.X)
                {
                    Collision(col, Direction.Right);
                }
            }
        }

        public override void HandleCollisions(List<BaseEntity> collisions)
        {
            IsAirborne = true;
            base.HandleCollisions(collisions);
        }

        protected virtual void Collision(BaseEntity col, Direction dir)
        {
            if(dir == Direction.Left)
            {
                SpeedX = 0;
                X = col.Hitbox.Right - 1;
            }
            else if(dir == Direction.Right)
            {
                SpeedX = 0;
                X = col.Hitbox.Left - Hitbox.Width - 1;
            }
            else if(dir == Direction.Up)
            {
                SpeedY = 0;
                Y = col.Hitbox.Bottom;
            }
            else if(dir == Direction.Down)
            {
                IsAirborne = false;
                SpeedY = 0;
                Y = col.Hitbox.Top - Hitbox.Height;
                //col.drawHitbox = true;
            }
            UpdateHitbox();
        }

        public override void Update(long elapsedTime)
        {
            if(IsAirborne)
            {
                this.SpeedY += Gravity * Weight;
            }
            base.Update(elapsedTime);

            if(this.SpeedX > 0)
            {
                SpeedX -= Decelleration * Weight;
                if (SpeedX < 0) SpeedX = 0;
            }
            else if(SpeedX < 0)
            {
                SpeedX += Decelleration * Weight;
                if (SpeedX > 9) SpeedX = 0;
            }
        }
    }
}
