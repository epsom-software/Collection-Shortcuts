using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionShortcuts.Tests
{
    [TestClass]
    public class FixCollectionTests_AccessData
    {

        FixCollection<AnnoyingPairs.NamespaceA.NameValuePair> Target;

        [TestInitialize]
        public void Init()
        {
            Target = new FixCollection<AnnoyingPairs.NamespaceA.NameValuePair>(
                "key0", "value0",
                "key1", "value1",
                "key2", "value2"
            );
        }

        [TestMethod]
        public void CanUseIndexToGetValue()
        {
            string value = Target["key1"];
            Assert.AreEqual("value1", value);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void WhenIndexIsGivenUnkownKeyThrowsIndexOutOfRangeException()
        {
            string value = Target["unknown"];
        }

        [TestMethod]
        public void CanUseIndexToSetNewKey()
        {
            Target["newKey"] = "newValue";
            string value = Target["newKey"];
            Assert.AreEqual("newValue", value);
        }

        [TestMethod]
        public void CanUseIndexToSetOldKey()
        {
            Target["key1"] = "newValue";
            string value = Target["key1"];
            Assert.AreEqual("newValue", value);
        }

        [TestMethod]
        public void TryGet_WhenGivenAUnknownKeyReturnsNull()
        {
            string value = Target.TryGet("unknown");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TryGet_WhenGivenAKnownKeyReturnsValue()
        {
            string value = Target.TryGet("key2");
            Assert.AreEqual("value2", value);
        }
    }
}
