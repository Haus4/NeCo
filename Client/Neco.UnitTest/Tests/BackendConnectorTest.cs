using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    [TestClass]
    public class BackendConnectorTest
    {
        Client.Network.BackendConnector connector;
        public BackendConnectorTest()
        {
            connector = new Client.Network.BackendConnector("google.com:80");
        }

        [TestMethod]
        public void ShouldBeConnected()
        {
            while(connector.CurrentState == Client.Core.State.Unknown)
            {
                Thread.Sleep(10);
            }

            Assert.IsTrue(connector.Connected);
        }
    }
}
