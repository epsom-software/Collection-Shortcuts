using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private readonly ICollection<T> Pairs;

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

            Pairs = new Collection<T>();

            for (int i = 0; i < keyValuePairs.Length; i += 2)
            {
                dynamic pair = new T();
                SetKey(pair, keyValuePairs[i]);
                pair.Value = keyValuePairs[i + 1];
                Pairs.Add(pair);
            }
        }

        public FixCollection(ICollection<T> pairs)
        {
            FixCollection<T> fixCollection = pairs as FixCollection<T>;
            if(fixCollection != null)
            {
                Pairs = fixCollection.Pairs;
            }
            else
            {
                Pairs = pairs;
            }
        }

        public static FixCollection<T> NewFixCollection<TSource>(ICollection<TSource> source) where TSource : new()
        {
            ICollection<T> collectionOfT = source as ICollection<T>;

            if (collectionOfT == null)
            {
                collectionOfT = new Collection<T>();
                
                foreach(dynamic s in source)
                {
                    dynamic t = new T();
                    string key = FixCollection<TSource>.GetKey(s);
                    SetKey(t, key);
                    t.Value = s.Value;
                    collectionOfT.Add(t);
                }
            }

            FixCollection<T> result = collectionOfT as FixCollection<T>;

            if (result == null)
            {
                result = new FixCollection<T>(collectionOfT);
            }

            return result;
        }

        void ICollection<T>.Add(T item)
        {
            Pairs.Add(item);
        }

        void ICollection<T>.Clear()
        {
            Pairs.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return Pairs.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            Pairs.CopyTo(array, arrayIndex);
        }

        int ICollection<T>.Count
        {
            get
            {
                return Pairs.Count ;
            }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return Pairs.IsReadOnly; }
        }

        bool ICollection<T>.Remove(T item)
        {
            T match;
            if (TryGet(GetKey(item), out match))
            {
                return Pairs.Remove(match);
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Pairs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Pairs.GetEnumerator();
        }

        public string this[string key]
        {
            get
            {
                T value;
                if (TryGet(key, out value))
                {
                    return (value as dynamic).Value;
                }
                else
                {
                    throw new IndexOutOfRangeException("The key index was not found in the colleciton. The key was " + key);
                }
            }
            set
            {
                T match;
                if(TryGet(key, out match))
                {
                    (match as dynamic).Value = value;
                }
                else
                {
                    match  = new T();
                    SetKey(match, key);
                    (match as dynamic).Value = value;
                    Pairs.Add(match);
                }
            }
        }

        public string TryGet(string key)
        {
            T value;
            if (TryGet(key, out value))
            {
                return (value as dynamic).Value;
            }
            else
            {
                return null;
            }
        }

        private bool TryGet(string key, out T value)
        {
            value = Pairs.FirstOrDefault(p => GetKey(p) == key);
            return value != null;
        }
    }
}
