using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            this.X = 0;
            this.Y = 0;
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
                this.X = (Actor.X + (Actor.Width / 2f)) - (Config.SCREEN_WIDTH / 2);
                this.Y = (Actor.Y + (Actor.Height / 2f)) - (Config.SCREEN_HEIGHT / 2);
            }
        }
    }
}
