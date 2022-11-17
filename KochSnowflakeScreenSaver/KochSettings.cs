using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochSnowflakeScreenSaver
{
    class KochSettings
    {
        private const int MAX_LEVEL = 4;

        private const double ADJUST_ANGLE = Math.PI / 2;

        public int MaxLevel { get; set; }

        public double AdjustAngle { get; set; }

        public KochSettings()
        {
            MaxLevel = MAX_LEVEL;
            AdjustAngle = ADJUST_ANGLE;
        }
    }
}
