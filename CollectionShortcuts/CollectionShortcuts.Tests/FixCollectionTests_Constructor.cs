using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class FixCollectionTests_Constructor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContructingWithNullThrowsArgumentNullException()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(null);
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
        public void CanContructACollectionWithThreePairs()
        {
            new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );
        }
    }
}
