using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src.game.entitites
{
    public class CameraEntity : DynamicEntity
    {
        private const float CAM_SPEED = 2f;

        public CameraEntity(float x, float y, float width, float height) : base(x, y, width, height)
        {
            base.SetTexture(GameTextures.CameraTexture);
            base.Layer = 5;
        }

        public override void Update(long elapsedTime)
        {
            if (InputController.IsKeyDown((int)Keys.A))
                this.X -= CAM_SPEED;
            else if (InputController.IsKeyDown((int)Keys.D))
                this.X += CAM_SPEED;
            if (InputController.IsKeyDown((int)Keys.W))
                this.Y -= CAM_SPEED;
            else if (InputController.IsKeyDown((int)Keys.S))
                this.Y += CAM_SPEED;
            base.Update(elapsedTime);
        }
    }
}
