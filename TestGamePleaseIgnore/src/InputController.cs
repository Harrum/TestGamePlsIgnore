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

        public InputController()
        {
            pressedKeys = new List<int>();
        }

        public static bool IsKeyDown(int key)
        {
            return pressedKeys.Contains(key);
        }

        internal void KeyDown(object sender, KeyEventArgs e)
        {
            int key = e.KeyValue;
            if (!pressedKeys.Contains(key)) pressedKeys.Add(key);
        }

        internal void KeyUp(object sender, KeyEventArgs e)
        {
            int key = e.KeyValue;
            if (pressedKeys.Contains(key)) pressedKeys.Remove(key);
        }
    }
}
