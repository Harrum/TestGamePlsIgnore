using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.game.entitites.Effects;

namespace TestGamePleaseIgnore.src.game.entitites
{
    public class Penguin : ControllableEntity
    {
        private const float WIDTH = 62;
        private const float HEIGHT = 60;

        private List<CollisionEffect> collisionEffects;

        public Penguin(float x, float y) : base(x, y, WIDTH, HEIGHT)
        {
            base.SetTexture(GameTextures.PenguinIdle);
            collisionEffects = new List<CollisionEffect>();
        }

        public override void HandleCollisions(List<BaseEntity> collisions)
        {
            foreach (CollisionEffect effect in collisionEffects)
            {
                Game.RemoveEntity(effect);
            }
            collisionEffects.Clear();
            base.HandleCollisions(collisions);
        }

        protected override void Collision(BaseEntity col, Direction dir)
        {
            base.Collision(col, dir);
            CollisionEffect effect = new CollisionEffect(col, dir);
            collisionEffects.Add(effect);
            Game.AddEntity(effect);
        }

        public override void Draw(RenderTarget g)
        {
            base.Draw(g);
        }
    }
}
