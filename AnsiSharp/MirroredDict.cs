using System.Collections.Generic;
using System.Linq;

namespace AnsiSharp
{
    public class MirroredDict<T1, T2> : IDictionary<T1, T2>
    {
        private readonly Dictionary<T1, T2> _lookup;
        private readonly Dictionary<T2, T1> _reverse;
 
        public MirroredDict()
        {
            _lookup = new Dictionary<T1, T2>();
            _reverse = new Dictionary<T2, T1>();
        }

        public void Add(T1 key, T2 value)
        {
            _lookup.Add(key, value);
            _reverse.Add(value, key);
        }

        public bool ContainsKey(T1 key)
        {
            return _lookup.ContainsKey(key);
        }

        public bool ContainsValue(T2 key)
        {
            return _reverse.ContainsKey(key);
        }

        public ICollection<T1> Keys
        {
            get { return _lookup.Keys; }
        }

        public bool Remove(T1 key)
        {
            T2 val;
            if (TryGetValue(key, out val))
            {
                _reverse.Remove(val);
            }
            return _lookup.Remove(key);
        }

        public bool Remove(T2 key)
        {
            T1 val;
            if (TryGetValue(key, out val))
            {
                _lookup.Remove(val);
            }
            return _reverse.Remove(key);
        }

        public bool TryGetValue(T1 key, out T2 value)
        {
            return _lookup.TryGetValue(key, out value);
        }

        public bool TryGetValue(T2 key, out T1 value)
        {
            return _reverse.TryGetValue(key, out value);
        }

        public ICollection<T2> Values
        {
            get { return _lookup.Values; }
        }

        public T2 this[T1 key]
        {
            get { return _lookup[key]; }
            set
            {
                Add(key, value);
            }
        }

        public void Add(KeyValuePair<T1, T2> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _lookup.Clear();
            _reverse.Clear();
        }

        public bool Contains(KeyValuePair<T1, T2> item)
        {
            return _lookup.Contains(item);
        }

        public void CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        {
            (_lookup as IDictionary<T1, T2>).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _lookup.Count; }
        }

        public bool IsReadOnly
        {
            get { return (_lookup as IDictionary<T1, T2>).IsReadOnly; }
        }

        public bool Remove(KeyValuePair<T1, T2> item)
        {
            if (_lookup.ContainsKey(item.Key) && _reverse.ContainsKey(item.Value))
            {
                return Remove(item.Key);
            }
            return false;
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _lookup.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _lookup.GetEnumerator();
        }
    }
}
