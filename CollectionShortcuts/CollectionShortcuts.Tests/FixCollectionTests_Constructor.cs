using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class FixCollectionTests_Constructor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContructingWithNullThrowsArgumentNullException()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>((string[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ContructingWithAnOddNumberOfParametersThrowsArgumentOutOfRangeException()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>("x");
        }

        [TestMethod]
        public void CanContructEmptyCollection()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>();
        }

        [TestMethod]
        public void CanContructWithThreePairs()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );
        }

        [TestMethod]
        public void CanContructWithAnotherCollection()
        {
            ICollection<AnnoyingPairs.NamespaceA.NameValuePair> source = new Collection<AnnoyingPairs.NamespaceA.NameValuePair>
            {
                new AnnoyingPairs.NamespaceA.NameValuePair
                {
                    Key = "key0", 
                    Value = "value0"
                }
            };

            var target = new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(source);
            Assert.AreEqual("key0", target.First().Key);
        }

        [TestMethod]
        public void WhenContructingWithAnotherFixCollectionDoNotIterateOverSource()
        {
            ICollection<AnnoyingPairs.NamespaceA.NameValuePair> source = new FixCollectionStubWhichCantBeEnumerated(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );

            var target = new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(source);
            Assert.AreEqual("key0", target.First().Key);
        }
    }
}
