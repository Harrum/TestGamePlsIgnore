using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;

namespace TestGamePleaseIgnore.src.Entity
{
    public class DynamicEntity : BaseEntity
    {
        protected float SpeedX { get; set; }
        protected float SpeedY { get; set; }

        public DynamicEntity(float x, float y, float width, float height) : base(x, y, width, height)
        {

        }

        public override void Update(long elapsedTime)
        {  
            X += SpeedX;
            Y += SpeedY;
            base.Update(elapsedTime);
            base.UpdateHitbox();
        }
    }
}
