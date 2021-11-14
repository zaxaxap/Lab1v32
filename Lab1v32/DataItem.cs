using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab1v3
{
    public partial struct DataItem: IComparable<DataItem>
    {
        public double x { get; set; }
        public double y { get; set; }
        public Vector2 component { get; set; }
        public DataItem(double x, double y, System.Numerics.Vector2 component)
        {
            this.x = x;
            this.y = y;
            this.component = component;
        }
        public string ToLongString(string format)
        {
            return ($"X = {x.ToString(format)}, Y = {y.ToString(format)}, Field X component = {component.X.ToString(format)}, Field Y component = {component.Y.ToString(format)}, Module of field = { Math.Sqrt(this.component.X * this.component.X + this.component.Y * this.component.Y).ToString(format)}\n");
        }
        public override string ToString()
        {
            return String.Format("Точка измерения: {0:f2}, {1:f2}\n Компоненты вектора поля и его модуль: {2:f2}, {3:f2}; {4:f3}", this.x, this.y, this.component.X, this.component.Y, Math.Sqrt(this.component.X * this.component.X + this.component.Y * this.component.Y));
        }

        public int CompareTo(DataItem other) => (x * x + y * y).CompareTo(other.x * other.x + other.y * other.y);
    }
}
