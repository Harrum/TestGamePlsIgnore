using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.Texture;

namespace TestGamePleaseIgnore.src.game
{
    public class Test
    {
        public class TestEntity : BaseEntity
        {
            public TestEntity(float x, float y, float width, float height) : base(x, y, width, height)
            {
                base.SetHitbox(new RectangleF(X + Width / 3, Y + Height / 3, Width / 3, Height / 3));
                base.SetTexture(new BaseTexture(GameTextures.TestBitmapRed));
                this.Layer = 3;
            }
        }

        public class TextureTestEntity : BaseEntity
        {
            public TextureTestEntity(float x, float y, float width, float height) : base(x, y, width, height)
            {
                base.SetTexture(new BaseTexture(GameTextures.TestBitmapGreen));
                this.Layer = 1;
            }
        }

        public class AnimatedTextureEntity : DynamicEntity
        {
            public AnimatedTextureEntity(float x, float y, float width, float height) : base(x, y, width, height)
            {
                base.SetTexture(new AnimatedTexture(GameTextures.TestAnimatedBitmap, 10, 10, 4));
                SpeedX = 3f;
                this.Layer = 2;
            }

            public override void Update(long elapsedTime)
            {
                base.Update(elapsedTime);

                if (X + Width > Config.SCREEN_WIDTH)
                    SpeedX = -3f;
                else if (X < 0)
                    SpeedX = 3f;
            }
        }

        TestEntity testEntity;
        TextureTestEntity textureTestEntity;
        AnimatedTextureEntity animatedTextureEntity;

        public Test()
        {
            testEntity = new TestEntity(20, 20, 60, 60);
            textureTestEntity = new TextureTestEntity(50, 30, 60, 60);
            animatedTextureEntity = new AnimatedTextureEntity(200, 20, 60, 60);
            Game.AddEntity(testEntity);
            Game.AddEntity(textureTestEntity);
            Game.AddEntity(animatedTextureEntity);
        }

        public void Draw(RenderTarget g)
        {

        }

        public void Update(long elapsedTime)
        {

        }
    }
}
