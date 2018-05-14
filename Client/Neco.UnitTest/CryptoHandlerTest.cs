using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Neco.UnitTest
{
    [TestClass]
    public class CryptoHandlerTest
    {
        MockDataStore dataStore;
        Client.Core.CryptoHandler handler;

        public CryptoHandlerTest()
        {
            dataStore = new MockDataStore();
            handler = new Client.Core.CryptoHandler(null, dataStore);
        }

        [TestMethod]
        public void ShouldGenerateKeys()
        {
            Assert.IsNotNull(dataStore.GetString(null, "publicKey"));
            Assert.IsNotNull(dataStore.GetString(null, "privateKey"));
        }

        [TestMethod]
        public void ShouldRegenerateKeysIfInvalid()
        {
            dataStore.SetString(null, "publicKey", "test");
            dataStore.SetString(null, "privateKey", "test");
            handler = new Client.Core.CryptoHandler(null, dataStore);

            Assert.AreNotEqual("test", dataStore.GetString(null, "publicKey"));
            Assert.AreNotEqual("test", dataStore.GetString(null, "privateKey"));
        }

        [TestMethod]
        public void ShouldVeriySignature()
        {
            var signature = handler.CalculateSignature("test");

            Assert.IsTrue(handler.VerifySignature("test", signature));
        }

        [TestMethod]
        public void ShouldFailVerifyingSignature()
        {
            var signature = handler.CalculateSignature("test");
            var signature2 = handler.CalculateSignature("test");
            signature2[0]++;

            Assert.IsFalse(handler.VerifySignature("test1", signature));
            Assert.IsFalse(handler.VerifySignature("test", signature2));
        }

        [TestMethod]
        public void ShouldVerifySecuritySignature()
        {
            var signature = handler.CalculateSecuritySignature();

            Assert.IsTrue(handler.VerifySecuritySignature(signature));
        }

        [TestMethod]
        public void ShouldSerializePublicKey()
        {
            Assert.IsNotNull(handler.SerializePublicKey());
        }
    }
}
