using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Lab1v3
{
    class V3DataList : V3Data
    {
        public List<DataItem> data { get; }
        public V3DataList(string id, DateTime date) : base(id, date)
        {
            data = new List<DataItem>();
        }
        public bool Add(DataItem newItem)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if ((data[i].x == newItem.x) && (data[i].y == newItem.y))
                {
                    return false;
                }
            }
            data.Add(newItem);
            return true;
        }
        public int AddDefaults(int nItems, FdblVector2 F)
        {
            int k = (int)Math.Floor(Math.Sqrt(nItems));
            int CountOfNew = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (this.Add(new DataItem(j, i, F(j, i))))
                    {
                        CountOfNew++;
                    }

                }
            }
            for (int i = 0; i < nItems - k * k; i++)
            {
                if (this.Add(new DataItem(k, i, F(k, i))))
                {
                    CountOfNew++;
                }
            }
            return CountOfNew;
        }
        public override int Count
        {
            get { return data.Count; }
        }
        public override double MaxDistance
        {
            get
            {
                double max = 0;
                double distance;
                for (int i = 0; i < (data.Count - 1); i++)
                {
                    for (int j = i + 1; j < data.Count; j++)
                    {
                        distance = Math.Sqrt((data[i].x - data[j].x) * (data[i].x - data[j].x) + (data[i].y - data[j].y) * (data[i].y - data[j].y));
                        if (distance > max)
                        {
                            max = distance;
                        }
                    }
                }
                return max;
            }
        }
        public override string ToString()
        {
            return "V3DataList\n" + base.ToString() + "Количество элементов: " + data.Count + "\n";
        }
        public override string ToLongString(string format)
        {
            StringBuilder str = new StringBuilder(ToString());
            for (int i = 0; i < data.Count; i++)
            {
                str.Append(data[i].ToLongString(format));
            }
            return str.ToString();

        }
        public override IEnumerator<DataItem> GetEnumerator()
        {
            foreach(DataItem item in data)
            {
                yield return item;
            }
        }
        public static bool SaveBinary(string filename, V3DataList v3)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(fs);

                writer.Write(v3.id);
                writer.Write(v3.date.ToString());
                foreach (var mesure in v3.data)
                {
                    writer.Write(mesure.x + " " + mesure.y + " " + mesure.component.X + " " + mesure.component.Y);
                }
                writer.Close();
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
        public static bool LoadBinary(string filename, ref V3DataList v3)
        {
            CultureInfo CI = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                BinaryReader reader = new BinaryReader(fs);

                v3.id = reader.ReadString();
                v3.date = DateTime.Parse(reader.ReadString(), CI);
                while (reader.BaseStream.Length != reader.BaseStream.Position)
                {
                    string[] measure = reader.ReadString().Split(' ');
                    v3.Add(new DataItem(double.Parse(measure[0], CI), double.Parse(measure[0], CI), new System.Numerics.Vector2(float.Parse(measure[2], CI), float.Parse(measure[3], CI))));
                }
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
    }
}
