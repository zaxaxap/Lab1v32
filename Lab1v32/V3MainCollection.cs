using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1v3
{
    partial class V3MainCollection
    {
        private List<V3Data> List = new List<V3Data>();
        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        public V3Data this[int index]
        {
            get => List[index];
        }
        public bool Contains(string ID)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].id == ID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Add(V3Data v3Data)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (v3Data.id == List[i].id)
                {
                    return false;
                }
            }
            List.Add(v3Data);
            return true;
        }
        public string ToLongString(string format)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                str.Append(List[i].ToLongString(format));
            }
            return str.ToString();
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < List.Count; i++)
            {
                str.Append(List[i].ToString());
            }
            return str.ToString();
        }
        public DataItem? Max
        {
            get
            {
                if (List.Count != 0)
                {
                    IEnumerable<DataItem> list = from elem in (from data in List
                                                               where data is V3DataList
                                                               select (V3DataList)data)
                                                 from item in elem
                                                 select item;
                    IEnumerable<DataItem> array = from elem in (from data in List
                                                                where data is V3DataArray
                                                                select (V3DataArray)data)
                                                  from item in elem
                                                  select item;
                    IEnumerable<DataItem> DataItems = array.Union(list);
                    return DataItems.Max();
                }
                return null;
            }
        }
        public IEnumerable<double> NUniqueX
        {
            get
            {
                if (List.Count() != 0)
                {
                    IEnumerable<DataItem> list = from elem in (from data in List
                                                               where data is V3DataList
                                                               select (V3DataList)data)
                                                 from item in elem
                                                 select item;
                    IEnumerable<DataItem> array = from elem in (from data in List
                                                                where data is V3DataArray
                                                                select (V3DataArray)data)
                                                  from item in elem
                                                  select item;
                    IEnumerable<DataItem> DataItems = array.Union(list);
                    var GroupedX = from X in DataItems
                                   group X by X.x into g
                                   select new { Name = g.Key, Count = g.Count() };
                    IEnumerable<double> SelectedX = from item in GroupedX where item.Count >= 2 select item.Name;
                    return SelectedX;
                }
                return null;
            }
        }
        public IEnumerable<V3Data> MinDate
        {
            get
            {
                if (List.Count() != 0)
                {
                    IEnumerable<V3Data> res = from elem in List where elem.date == List.Min().date select elem;
                    return res;
                }
                return null;
            }
        }    
    }
}
