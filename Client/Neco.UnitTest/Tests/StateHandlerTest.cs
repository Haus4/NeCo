using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    [TestClass]
    public class StateHandlerTest
    {
        SimpleStateHandler stateHandler;

        public StateHandlerTest()
        {
            stateHandler = new SimpleStateHandler();
        }

        [TestMethod]
        public void ShouldPropagateStateChanges()
        {
            bool changed = false;

            stateHandler.StateChanged += delegate
            {
                changed = true;
            };

            stateHandler.SetState(Client.Core.State.Success);

            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void ShouldNotPropagateStateChanges()
        {
            bool changed = false;

            stateHandler.StateChanged += delegate
            {
                changed = true;
            };

            stateHandler.SetState(Client.Core.State.Success);
            changed = false;
            stateHandler.SetState(Client.Core.State.Success);

            Assert.IsFalse(changed);
        }
    }
}
