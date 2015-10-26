using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src
{
    public enum DebugMode
    {
        NONE, DISPLAY_RECT, DISPLAY_HITBOX
    }

    public class Config
    {
        public static float SCREEN_WIDTH = 1280;
        public static float SCREEN_HEIGHT = 720;

        public static RectangleF SCREEN_RECT = new RectangleF(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

        public static DebugMode DEBUG_MODE = DebugMode.NONE;
    }
}
