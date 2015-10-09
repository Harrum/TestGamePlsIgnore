using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src
{
    public interface RunnableComponent
    {
        void Update(long elapsedTime);

        void Draw(RenderTarget g);
    }
}
