using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGamePleaseIgnore.src
{
    public class InputController
    {
        private static List<int> pressedKeys;
        private static List<int> releasedKeys;

        public InputController()
        {
            pressedKeys = new List<int>();
            releasedKeys = new List<int>();
        }

        public static bool IsKeyDown(int key)
        {
            return pressedKeys.Contains(key);
        }

        public static bool IsKeyPressed(int key)
        {
            return releasedKeys.Contains(key);
        }

        public void Update()
        {
            releasedKeys.Clear();
        }

        internal void KeyDown(object sender, KeyEventArgs e)
        {
            int key = e.KeyValue;
            if (!pressedKeys.Contains(key)) pressedKeys.Add(key);
        }

        internal void KeyUp(object sender, KeyEventArgs e)
        {
            int key = e.KeyValue;
            if (pressedKeys.Contains(key))
            {
                pressedKeys.Remove(key);
                releasedKeys.Add(key);
            }
        }
    }
}
