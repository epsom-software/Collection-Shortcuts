using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionShortcuts.Tests.AnnoyingPairs.NamespaceA;

namespace CollectionShortcuts.Tests
{
    public class FixCollectionStubWhichCantBeEnumerated : FixCollection<NameValuePair>, IEnumerable<NameValuePair>
    {
        public FixCollectionStubWhichCantBeEnumerated(params string[] keyValuePairs)
            : base(keyValuePairs)
        {
        }

        IEnumerator<NameValuePair> IEnumerable<NameValuePair>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
