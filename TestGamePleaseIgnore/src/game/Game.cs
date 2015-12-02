using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src.game
{
    public class Game : RunnableComponent
    {
        //todo create static entity list with layers.

        //Camera camera;
        //Level level;
        GameTextures gameTextures;
        //Test test;
        Level level;
        DebugOverlay debugOverlay;
        EntityController entityController;

        public Game()
        {
            //camera = new Camera(0, 0);
            //level = new Level();
            //level.LoadLevel();
            gameTextures = new GameTextures();
            level = new Level();
            entityController = EntityController.GetInstance();
        }

        public override void Initialize()
        {
            debugOverlay = new DebugOverlay();
        }

        public override void LoadContent(RenderTarget g)
        {
            gameTextures.LoadTextures(g);
            level.LoadLevel("level1");
            SetGameAreaSize(level.GetBounds());
            //test = new Test();
        }

        public override void Draw(RenderTarget g)
        {
            base.Draw(g);
            debugOverlay.Draw(g);
        }

        public override void Update(long elapsedTime)
        {
            entityController.Update(elapsedTime);
            base.Update(elapsedTime);
            debugOverlay.Update(elapsedTime);
            if (InputController.IsKeyPressed(82))
                level.ResetLevel();
        }

        static int Main(string[] args)
        {
            Game game = new Game();     
            TestGamePleaseIgnore mainLoop = new TestGamePleaseIgnore("Test Game Please Ignore", game);
            //game.Initialize();
            mainLoop.Start();
            return 0;
        }
    }
}
