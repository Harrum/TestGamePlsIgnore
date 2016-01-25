using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src.game.entitites.Effects
{
    public class CollisionEffect : BaseEntity
    {
        public CollisionEffect(BaseEntity col, Direction dir)
        {
            Layer = 5;

            Width = col.Width;
            Height = col.Height;
            switch (dir)
            {
                case Direction.Left:
                    SetTexture(GameTextures.ArrowRight);
                    X = col.X - col.Width;
                    Y = col.Y;
                    break;
                case Direction.Right:
                    SetTexture(GameTextures.ArrowRight);
                    Mirrored = true;
                    X = col.X + col.Width;
                    Y = col.Y;
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    SetTexture(GameTextures.ArrowUp);
                    X = col.X;
                    Y = col.Y + col.Height;
                    break;
                default:
                    break;
            }
        }
    }
}
