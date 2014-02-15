using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class CollectionExtenstionTests
    {
        [TestMethod]
        public void CanConvertTargetToFixCollection()
        {
            ICollection<AnnoyingPairs.NamespaceA.NameValuePair> target = new Collection<AnnoyingPairs.NamespaceA.NameValuePair>
            {
                new AnnoyingPairs.NamespaceA.NameValuePair
                {
                    Key = "key0", 
                    Value="value0"
                }
            };

            FixCollection<AnnoyingPairs.NamespaceA.NameValuePair> result = target.Fix();
            Assert.IsNotNull(result);
        }
    }
}
