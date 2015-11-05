using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.Texture;

namespace TestGamePleaseIgnore.src.game.entitites.blocks
{
    public class Block : BaseEntity
    {
        public const float WIDTH = 50f;
        public const float HEIGHT = 50f;

        public Block(float x, float y, BaseTexture texture) : base(x, y, WIDTH, HEIGHT)
        {
            base.SetTexture(texture);
        }
    }

    public class FallingBlock : GravityEntity
    {
        public FallingBlock(float x, float y, BaseTexture texture) : base(x, y, Block.WIDTH, Block.HEIGHT)
        {
            this.IsAirborne = true;
            base.SetTexture(texture);
            this.Weight = 0.1f;
        }
    }

    public class BouncyBlock : GravityEntity
    {
        private float speed = 1f;

        public BouncyBlock(float x, float y, BaseTexture texture) : base(x, y, Block.WIDTH, Block.HEIGHT)
        {
            this.SpeedX = speed;
            base.SetTexture(texture);
        }

        protected override void CheckCollision(BaseEntity col)
        {
            if (this.Hitbox.Bottom > col.Hitbox.Top && this.Hitbox.Top < col.Hitbox.Bottom)
            {
                if (this.Hitbox.Right >= col.Hitbox.Left && this.Hitbox.Left < col.Hitbox.Left)
                {
                    this.SpeedX = -speed;
                    //col.drawHitbox = true;
                }
                else if (this.Hitbox.Left <= col.Hitbox.Right && this.Hitbox.Right > col.Hitbox.Right)
                {
                    this.SpeedX = speed;
                    //col.drawHitbox = true;
                }
            }
        }
    }
}
