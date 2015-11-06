using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src
{
    public class Camera
    {
        private BaseEntity Actor;
        private static Camera Instance;

        public float X { private set; get; }
        public float Y { private set; get; }

        public static Camera GetInstance()
        {
            if (Instance == null)
                Instance = new Camera();
            return Instance;
        }

        private Camera()
        {
            Actor = null;
        }

        public void SetActor(BaseEntity actor)
        {
            this.Actor = actor;
        }

        public void RemoveActor()
        {
            this.Actor = null;
        }

        public void Update()
        {
            if (this.Actor != null)
            {
                this.X = Actor.X - (Config.SCREEN_WIDTH / 2);
                this.Y = Actor.Y - (Config.SCREEN_HEIGHT / 2);
            }
            else
            {
                this.X = 0;
                this.Y = 0;
            }
        }
    }
}
