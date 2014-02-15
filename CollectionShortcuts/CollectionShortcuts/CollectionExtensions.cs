using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionShortcuts
{
    public static class CollectionExtensions
    {
        public static FixCollection<T> Fix<T>(this ICollection<T> source) where T : new()
        {
            return new FixCollection<T>(source);
        }
    }
}
