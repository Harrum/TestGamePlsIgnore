using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using TestGamePleaseIgnore.src.Entity;
using TestGamePleaseIgnore.src.Texture;

namespace TestGamePleaseIgnore.src.game
{
    public class SpriteTester : RunnableComponent
    {
        public class SpriteTestEntity : BaseEntity
        {
            public SpriteTestEntity(float x, float y, float width, float height) : base(x, y, width, height)
            {

            }

            public void ChangeTexture(BaseTexture texture)
            {
                base.SetTexture(texture);
            }
        }

        private SpriteTestEntity testEntity;

        public override void Initialize()
        {

        }

        public override void LoadContent(RenderTarget g)
        {

        }

        static int Main(string[] args)
        {
            Config.SCREEN_WIDTH = 400;
            Config.SCREEN_HEIGHT = 400;
            SpriteTester tester = new SpriteTester();
            TestGamePleaseIgnore mainLoop = new TestGamePleaseIgnore("Sprite Tester", tester);
            //game.Initialize();
            mainLoop.Start();
            return 0;
        }
    }
}
