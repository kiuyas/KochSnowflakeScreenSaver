using System;
using System.Collections.Generic;
using System.Drawing;

namespace KochSnowflakeScreenSaver
{
    class KochGenerator
    {
        private const double thetaOf60Degree = 60 * Math.PI / 180;

        private KochSettings settings = null;

        public void Execute(KochData data, KochSettings settings)
        {
            MakeKochSnowflake(data, settings);
        }

        private void MakeKochSnowflake(KochData data, KochSettings settings)
        {
            this.settings = settings;

            var list = new List<Point[]>();
            double delta = Math.PI * 2 / data.NumberOfSides;

            for (double theta = 0; theta < Math.PI * 2; theta += delta)
            {
                double angle = theta + settings.AdjustAngle + data.AdjustAngle;
                double x1 = data.CX + Math.Cos(angle) * data.R;
                double y1 = data.CY + Math.Sin(angle) * data.R;
                double x2 = data.CX + Math.Cos(angle + delta) * data.R;
                double y2 = data.CY + Math.Sin(angle + delta) * data.R;
                MakeKochCurve(list, x1, y1, x2, y2, 0);
            }

            data.KochList = list;
        }

        private void MakeKochCurve(List<Point[]> list, double x1, double y1, double x2, double y2, int level)
        {
            if (level == settings.MaxLevel)
            {
                list.Add(GetPoints(x1, y1, x2, y2));
            }
            else
            {
                double vx = (x2 - x1) / 3;
                double vy = (y2 - y1) / 3;

                double xx1 = x1 + vx;
                double yy1 = y1 + vy;

                double[] v1 = rotate(thetaOf60Degree, vx, vy);

                double xx2 = xx1 + v1[0];
                double yy2 = yy1 + v1[1];

                double[] v2 = rotate(-thetaOf60Degree, vx, vy);

                double xx3 = xx2 + v2[0];
                double yy3 = yy2 + v2[1];

                level++;

                MakeKochCurve(list, x1, y1, xx1, yy1, level);
                MakeKochCurve(list, xx1, yy1, xx2, yy2, level);
                MakeKochCurve(list, xx2, yy2, xx3, yy3, level);
                MakeKochCurve(list, xx3, yy3, x2, y2, level);
            }
        }

        private Point[] GetPoints(double x1, double y1, double x2, double y2)
        {
            return new Point[] { new Point(T(x1), T(y1)), new Point(T(x2), T(y2)) };
        }

        private int T(double d)
        {
            return (int)d;
        }

        private double[] rotate(double theta, double x, double y)
        {
            double sinTheta = Math.Sin(theta);
            double cosTheta = Math.Cos(theta);
            double x2 = cosTheta * x - sinTheta * y;
            double y2 = sinTheta * x + cosTheta * y;
            return new double[] { x2, y2 };
        }
    }
}
