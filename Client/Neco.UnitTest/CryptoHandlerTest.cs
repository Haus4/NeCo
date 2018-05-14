using System;
using Xunit;

namespace Neco.UnitTest
{
    public class CryptoHandlerTest
    {
        MockDataStore dataStore;
        Client.Core.CryptoHandler handler;

        public CryptoHandlerTest()
        {
            dataStore = new MockDataStore();
            handler = new Client.Core.CryptoHandler(null, dataStore);
        }

        [Fact]
        public void ShouldGenerateKeys()
        {
            Assert.NotNull(dataStore.GetString(null, "publicKey"));
            Assert.NotNull(dataStore.GetString(null, "privateKey"));
        }

        [Fact]
        public void ShouldNotGenerateKeysIfStored()
        {
            dataStore = new MockDataStore();
            dataStore.SetString(null, "publicKey", "test");
            dataStore.SetString(null, "privateKey", "test");
            handler = new Client.Core.CryptoHandler(null, dataStore);

            Assert.Equal("test", dataStore.GetString(null, "publicKey"));
            Assert.Equal("test", dataStore.GetString(null, "privateKey"));
        }
    }
}
