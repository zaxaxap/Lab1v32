using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace Lab1v3
{
    class V3DataArray : V3Data, IEnumerable<DataItem>
    {
        public Vector2[,] dimensions { get; private set; }
        public double stepX { get; private set; }
        public double stepY { get; private set; }
        public int nodesX { get; private set; }
        public int nodesY { get; private set; }
        public V3DataArray(string id, DateTime date) : base(id, date)
        {
            dimensions = new Vector2[0, 0];
        }
        public V3DataArray(string id, DateTime date, int nodesX, int nodesY, double stepX, double stepY, FdblVector2 F) : base(id, date)
        {
            dimensions = new Vector2[nodesY, nodesX];
            this.stepX = stepX;
            this.nodesX = nodesX;
            this.nodesY = nodesY;
            this.stepY = stepY;
            for (int i = 0; i < nodesY; i++)
            {
                for (int j = 0; j < nodesX; j++)
                {
                    dimensions[i, j] = F(i * stepY, j * stepX);
                }
            }
        }
        public override int Count
        {
            get
            {
                return nodesX * nodesY;
            }
        }
        public override double MaxDistance
        {
            get
            {
                return Math.Sqrt((nodesX - 1) * stepX * (nodesX - 1) * stepX + (nodesY - 1) * stepY * (nodesY - 1) * stepY);
            }
        }
        public override string ToString()
        {
            return "V3DataArray\n" + base.ToString() + "Размер шага по X и по Y:" + stepX + " " + stepY + "\n" + "Размер сетки: " + nodesX + "*" + nodesY + "\n";
        }
        public override string ToLongString(string format)
        {
            StringBuilder str = new StringBuilder(ToString());
            for (int i = 0; i < nodesY; i++)
            {
                for (int j = 0; j < nodesX; j++)
                {
                    str.Append($"X = {(j * stepX).ToString(format)}, Y = {(i * stepY).ToString(format)}, Field X component = {dimensions[i, j].X.ToString(format)}, Field Y component = {dimensions[i, j].Y.ToString(format)}, Module of field = { Math.Sqrt(dimensions[i, j].X * dimensions[i, j].X + dimensions[i, j].Y * dimensions[i, j].Y).ToString(format)}\n"); //Тут была ошибка в выводе
                }
            }

            return str.ToString();
        }
        public static explicit operator V3DataList(V3DataArray param)
        {
            V3DataList List = new V3DataList(param.id, param.date);
            for (int i = 0; i < param.nodesY; i++)
            {
                for (int j = 0; j < param.nodesX; j++)
                {
                    List.Add(new DataItem(j * param.stepX, i * param.stepY, param.dimensions[i, j]));
                }
            }
            return List;
        }
        public override IEnumerator<DataItem> GetEnumerator()
        {
            for(int j = 0; j < nodesY; j++)
                for(int i = 0; i < nodesX; i++)
                {
                    double x = stepX * i;
                    double y = stepY * j;
                    yield return new DataItem(x, y, dimensions[j, i]);
                }
        }
        public static bool SaveAsText(string filename, V3DataArray v3)
        {
            FileStream fs = null;
            try
            {
                fs= new FileStream(filename, FileMode.Truncate);
                StreamWriter stream = new StreamWriter(fs);
                stream.WriteLine(v3.id);
                stream.WriteLine(v3.date);
                stream.WriteLine(v3.stepX);
                stream.WriteLine(v3.stepY);
                stream.WriteLine(v3.nodesX);
                stream.WriteLine(v3.nodesY);
                foreach (var mesure in v3.dimensions)
                {
                    stream.WriteLine(mesure.X + " " + mesure.Y);
                }
                stream.Close();
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }
        public static bool LoadAsText(string filename, ref V3DataArray v3)
        {
            CultureInfo CI = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader stream = new StreamReader(fs);
                v3.id = stream.ReadLine();
                v3.date = DateTime.Parse(stream.ReadLine(), CI);
                v3.stepX = double.Parse(stream.ReadLine(), CI);
                v3.stepY = double.Parse(stream.ReadLine(), CI);
                v3.nodesX = int.Parse(stream.ReadLine(), CI);
                v3.nodesY = int.Parse(stream.ReadLine(), CI);
                v3.dimensions = new Vector2[v3.nodesY, v3.nodesX];
                string[] measure = null;
                for (int i = 0; i < v3.nodesY; i++)
                {
                    for(int j = 0; j < v3.nodesX; j++)
                    {
                        measure = stream.ReadLine().Split(' ');
                        v3.dimensions[i, j] = new Vector2(float.Parse(measure[0], CI), float.Parse(measure[1], CI)); 
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }
    }

}
