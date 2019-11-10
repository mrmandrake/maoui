using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Maoui
{
    class ReadOnlyList<T> : IReadOnlyList<T>
    {
        readonly List<T> list;

        public ReadOnlyList(List<T> items)
        {
            list = new List<T>(items);
        }

        T IReadOnlyList<T>.this[int index] => list[index];

        int IReadOnlyCollection<T>.Count => list.Count;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }
}
