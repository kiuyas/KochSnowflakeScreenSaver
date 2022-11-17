using System.Collections.Generic;
using System.Drawing;

namespace KochSnowflakeScreenSaver
{
    class KochData
    {
        public double CX { get; set; }

        public double CY { get; set; }

        public double R { get; set; }
        public double AdjustAngle { get; set; }

        public int NumberOfSides { get; set; }

        public List<Point[]> KochList { get; set; }

        public Pen DrawPen { get; set; }

        public KochData()
        {
            CX = 320;
            CY = 240;
            R = 100;
            NumberOfSides = 6;
            DrawPen = Pens.Yellow;
        }

        public KochData(double cx, double cy, double r,  int sides, double adjustAngle, Pen pen)
        {
            CX = cx;
            CY = cy;
            R = r;
            AdjustAngle = adjustAngle;
            NumberOfSides = sides;
            DrawPen = pen;
        }
    }
}
