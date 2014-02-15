using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class FixCollectionTests_ICollection
    {
        ICollection<AnnoyingPairs.NamespaceA.NameValuePair> Target;
        AnnoyingPairs.NamespaceA.NameValuePair ExamplePair;

        [TestInitialize]
        public void Init()
        {
            Target = new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );

            ExamplePair = new AnnoyingPairs.NamespaceA.NameValuePair { Name = "testKey", Value = "testValue" };
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
            bool success = Target.Remove(new AnnoyingPairs.NamespaceA.NameValuePair { Key = "key1", Value = "testValue" });
            Assert.IsTrue(success);
            Assert.AreEqual(2, Target.Count);
        }

        [TestMethod]
        public void GetEnumeratorGeneric()
        {
            IEnumerator<AnnoyingPairs.NamespaceA.NameValuePair> enumerator = Target.GetEnumerator();
            
            enumerator.MoveNext();
            enumerator.MoveNext();

            AnnoyingPairs.NamespaceA.NameValuePair pair = enumerator.Current;
            Assert.AreEqual("key1", pair.Key);
            Assert.AreEqual("value1", pair.Value);
        }

        [TestMethod]
        public void GetEnumeratorNonGeneric()
        {
            IEnumerator enumerator = (Target as IEnumerable).GetEnumerator();

            enumerator.MoveNext();
            enumerator.MoveNext();

            AnnoyingPairs.NamespaceA.NameValuePair pair = (AnnoyingPairs.NamespaceA.NameValuePair)enumerator.Current;
            Assert.AreEqual("key1", pair.Key);
            Assert.AreEqual("value1", pair.Value);
        }
    }
}
