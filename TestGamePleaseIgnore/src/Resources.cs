using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGamePleaseIgnore.src
{
    public class Resources
    {
        public static string RESOURCE_PATH = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Resources\\";

        public static SolidColorBrush SCBRUSH_RED;
        public static SharpDX.DirectWrite.Factory WRITE_FACTORY;
        public static TextFormat TEXT_FORMAT;
        public static Brush TEXT_BRUSH;

        public static void Initialize(RenderTarget g)
        {
            SCBRUSH_RED = new SolidColorBrush(g, Color.Red);
            WRITE_FACTORY = new SharpDX.DirectWrite.Factory();
            TEXT_FORMAT = new TextFormat(WRITE_FACTORY, "Arial", 14);
            TEXT_BRUSH = new SolidColorBrush(g, Color.Red);
        }
    }
}
