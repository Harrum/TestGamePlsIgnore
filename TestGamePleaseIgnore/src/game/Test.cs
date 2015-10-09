using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src.game
{
    public class Test
    {
        BaseEntity testEntity;

        public Test()
        {
            testEntity = new BaseEntity(20, 20, 60, 60);
        }

        public void Draw(RenderTarget g)
        {
            testEntity.Draw(g);
        }

        public void Update(long elapsedTime)
        {

        }
    }
}
