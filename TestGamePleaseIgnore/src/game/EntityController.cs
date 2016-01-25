using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGamePleaseIgnore.src.Entity;

namespace TestGamePleaseIgnore.src.game
{
    public class EntityController
    {
        private static EntityController instance;
        private ControllableEntity puppet;

        public static EntityController GetInstance()
        {
            if (instance == null)
                instance = new EntityController();
            return instance;
        }

        private EntityController()
        {
            puppet = null;
        }

        public void SetPuppetEntity(ControllableEntity puppet)
        {
            this.puppet = puppet;
        }

        public void RemovePuppetEntity()
        {
            this.puppet = null;
        }

        public void Update(long elapsedTime)
        {
            if (puppet != null)
            {
                if (InputController.IsKeyDown((int)Keys.A))
                    puppet.MoveLeft();
                else if (InputController.IsKeyDown((int)Keys.D))
                    puppet.MoveRight();
                if (InputController.IsKeyDown((int)Keys.Space))
                    puppet.Jump();
            }
        }
    }
}
