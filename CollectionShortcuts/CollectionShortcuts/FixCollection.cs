using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionShortcuts
{
    public class FixCollection<T> : ICollection<T> where T : new()
    {
        private static readonly Func<dynamic, string> GetKey;
        private static readonly Action<dynamic, string> SetKey;

        static FixCollection()
        {
            switch (typeof(T).Name)
            {
                case "TypeValuePair":
                    GetKey = pair => pair.Type;
                    SetKey = (pair, key) => pair.Type = key;
                    break;

                case "NameValuePair":
                    GetKey = pair => pair.Name;
                    SetKey = (pair, key) => pair.Name = key;
                    break;

                default:
                    throw new InvalidOperationException("FixCollection<T> was used with an invalid T.  T was " + typeof(T));
            }
        }

        private readonly List<string> Pairs;

        public FixCollection(params string[] keyValuePairs)
        {
            if (keyValuePairs == null)
            {
                throw new ArgumentNullException("keyValuePairs");
            }
            if (keyValuePairs.Length % 2 != 0)
            {
                throw new ArgumentOutOfRangeException("keyValuePairs", "The length of arguments must be even, so that they can be paired.  The length was " + keyValuePairs.Length);
            }

            Pairs = keyValuePairs.ToList();
        }

        private FixCollection(List<string> pairs)
        {
            Pairs = pairs;
        }

        public FixCollection(ICollection<T> source)
        {
            FixCollection<T> fixSource = source as FixCollection<T>;

            if (fixSource == null)
            {
                Pairs = new List<string>(source.Count * 2);

                foreach (dynamic s in source)
                {
                    Pairs.Add(GetKey(s));
                    Pairs.Add(s.Value);
                }
            }
            else
            {
                Pairs = fixSource.Pairs;
            }
        }

        public static FixCollection<T> NewFixCollection<TSource>(ICollection<TSource> source) where TSource : new()
        {
            FixCollection<TSource> fixSource = source as FixCollection<TSource>;

            if (fixSource == null)
            {
                fixSource = new FixCollection<TSource>(source);
            }

            FixCollection<T> result = fixSource as FixCollection<T>;

            if (result == null)
            {
                result = new FixCollection<T>(fixSource.Pairs);
            }

            return result;
        }

        void ICollection<T>.Add(T item)
        {
            dynamic pair = item;

            string key = GetKey(pair);
            string value = pair.Value;

            Pairs.Add(key);
            Pairs.Add(value);
        }

        void ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<T>.Contains(T item)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<T>.Count
        {
            get
            {
                return Pairs.Count / 2;
            }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<T>.Remove(T item)
        {
            dynamic pair = item;
            string key = GetKey(pair);

            for (int i = 0; i < Pairs.Count; i += 2)
            {
                if (Pairs[i] == key)
                {
                    Pairs.RemoveRange(i, 2);
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Pairs.Count; i += 2)
            {
                dynamic result = new T();
                SetKey(result, Pairs[i]);
                result.Value = Pairs[i + 1];
                yield return result;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
