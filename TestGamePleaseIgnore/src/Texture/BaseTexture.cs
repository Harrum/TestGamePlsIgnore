using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src.Texture
{
    public class BaseTexture
    {
        public Bitmap Texture { get; protected set; }

        public BaseTexture(Bitmap texture)
        {
            this.Texture = texture;
        }

        public virtual void Update(long elapsedTime)
        {

        }

        public virtual void DrawTexture(RenderTarget g, float x, float y, float width, float height)
        {
            g.DrawBitmap(Texture, new RectangleF(x, y, width, height), 1, BitmapInterpolationMode.NearestNeighbor);
        }
    }
}
