using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src.game
{
    public class Game : RunnableComponent
    {
        //Camera camera;
        //Level level;
        Test test;

        public Game()
        {
            //camera = new Camera(0, 0);
            //level = new Level();
            //level.LoadLevel();
            test = new Test();
        }

        public void Update(long elapsedTime)
        {
            //camera.Update(elapsedTime);
            //level.Update(elapsedTime);
            test.Update(elapsedTime);
        }

        public void Draw(RenderTarget g)
        {
            //level.Draw(g);
            test.Draw(g);
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
