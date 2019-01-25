using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotL.Units;

namespace TotL
{
    class Cluster : IList<Unit>
    {
        List<Unit> cluster = new List<Unit>();

        public UI.ClusterStatus Status { get; set; }
        public bool Summon { get; set; }
        public short Size { get; set; }

        public short Tick { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public bool HasTarget { get; set; }
        public short TargetIndex { get; set; }
        public short TargetTick { get; set; }

        public Cluster()
        {
            Tick = 0;
            HasTarget = false;
            TargetIndex = 0;
            TargetTick = 0;

        }
        public Unit this[int index] { get => cluster[index]; set { cluster[index] = value; } }

        public int Count => cluster.Count;

        public bool IsReadOnly => false;

        public void Add(Unit item)
        {
            cluster.Add(item);
        }

        public void Clear()
        {
            cluster.Clear();
        }

        public bool Contains(Unit item)
        {
            return cluster.Contains(item);
        }

        public void CopyTo(Unit[] array, int arrayIndex)
        {
            cluster.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return cluster.GetEnumerator();
        }

        public int IndexOf(Unit item)
        {
            return cluster.IndexOf(item);
        }

        public void Insert(int index, Unit item)
        {
            cluster.Insert(index, item);
        }

        public bool Remove(Unit item)
        {
            return cluster.Remove(item);
        }

        public void RemoveAt(int index)
        {
            cluster.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cluster.GetEnumerator();
        }
    }
}
