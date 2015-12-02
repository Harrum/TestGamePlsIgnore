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

        public virtual void DrawTexture(RenderTarget g, RectangleF position)
        {
            g.DrawBitmap(Texture, position, 1, BitmapInterpolationMode.NearestNeighbor);
        }
    }
}
