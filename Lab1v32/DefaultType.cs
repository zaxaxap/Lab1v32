using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1v3
{
    static class DefaultType
    {
        public static Vector2 AsCoordinat(double x, double y)
        {
            return new Vector2((float)x, (float)y);
        }
        public static Vector2 normalXplate(double x, double y)
        {
            return new Vector2(0, 1);
        }
        public static Vector2 zeros(double x, double y)
        {
            return new Vector2(0, 0);
        }
        public static Vector2 normalPoint(double x, double y)
        {
            return new Vector2((float)(Math.Cos(x / Math.Sqrt(x * x + y * y)) / (x * x + y * y)), (float)(Math.Cos(y / Math.Sqrt(x * x + y * y)) / (x * x + y * y)));
        }
    }
}
