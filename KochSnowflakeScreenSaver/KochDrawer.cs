using System.Collections.Generic;
using System.Drawing;

namespace KochSnowflakeScreenSaver
{
    class KochDrawer
    {
        public static void Execute(Graphics g, List<KochData> list)
        {
            foreach (KochData data in list)
            {
                foreach (Point[] pp in data.KochList)
                {
                    g.DrawLine(data.DrawPen, pp[0].X, pp[0].Y, pp[1].X, pp[1].Y);
                }
            }
        }
    }
}
