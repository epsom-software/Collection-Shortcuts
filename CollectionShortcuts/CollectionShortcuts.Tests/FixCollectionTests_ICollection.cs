using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;
using CollectionShortcuts.Tests.AnnoyingPairs;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class FixCollectionTests_ICollection_NamespaceANameValuePair : FixCollectionTests_ICollection<AnnoyingPairs.NamespaceA.NameValuePair>
    {
    }

    [TestClass]
    public class FixCollectionTests_ICollection_NamespaceATypeValuePair : FixCollectionTests_ICollection<AnnoyingPairs.NamespaceA.TypeValuePair>
    {
    }

    [TestClass]
    public class FixCollectionTests_ICollection_NamespaceBNameValuePair : FixCollectionTests_ICollection<AnnoyingPairs.NamespaceB.NameValuePair>
    {
    }

    [TestClass]
    public class FixCollectionTests_ICollection_NamespaceBTypeValuePair : FixCollectionTests_ICollection<AnnoyingPairs.NamespaceB.TypeValuePair>
    {
    }

    public class FixCollectionTests_ICollection<TPair> where TPair : IPair, new()
    {
        ICollection<TPair> Target;
        TPair ExamplePair;

        [TestInitialize]
        public void Init()
        {
            Target = new FixCollection<TPair>(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );

            ExamplePair = new TPair { Key = "testKey", Value = "testValue" };
        }

        [TestMethod]
        public void Count()
        {
            Assert.AreEqual(3, Target.Count);
        }

        [TestMethod]
        public void Add()
        {
            Target.Add(ExamplePair);
            Assert.AreEqual(4, Target.Count);
        }

        [TestMethod]
        public void IsReadOnly()
        {
            Assert.IsFalse(Target.IsReadOnly);
        }


        [TestMethod]
        public void WhenGivenUnknownKeyRemoveReturnsFalse()
        {
            bool success = Target.Remove(ExamplePair);
            Assert.IsFalse(success);
            Assert.AreEqual(3, Target.Count);
        }

        [TestMethod]
        public void WhenGivenKnownKeyRemoveReturnsTrue()
        {
            bool success = Target.Remove(new TPair { Key = "key1", Value = "testValue" });
            Assert.IsTrue(success);
            Assert.AreEqual(2, Target.Count);
        }

        [TestMethod]
        public void GetEnumeratorGeneric()
        {
            IEnumerator<TPair> enumerator = Target.GetEnumerator();
            
            enumerator.MoveNext();
            enumerator.MoveNext();

            TPair pair = enumerator.Current;
            Assert.AreEqual("key1", pair.Key);
            Assert.AreEqual("value1", pair.Value);
        }

        [TestMethod]
        public void GetEnumeratorNonGeneric()
        {
            IEnumerator enumerator = (Target as IEnumerable).GetEnumerator();

            enumerator.MoveNext();
            enumerator.MoveNext();

            TPair pair = (TPair)enumerator.Current;
            Assert.AreEqual("key1", pair.Key);
            Assert.AreEqual("value1", pair.Value);
        }
    }
}
