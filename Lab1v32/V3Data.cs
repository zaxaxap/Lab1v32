using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1v3
{
    abstract class V3Data: IEnumerable<DataItem>, IComparable<V3Data>
    {
        public string id { get; protected set; }
        public DateTime date { get; protected set; }
        public V3Data(string id, DateTime date)
        {
            this.id = id;
            this.date = date;
        }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }
        public abstract string ToLongString(string format);
        public override string ToString()
        {
            return String.Format("Идентификатор: {0} \nДата измерения: {1}\n", id, date);
        }

        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(V3Data other) => date.CompareTo(other.date);
    }
}
