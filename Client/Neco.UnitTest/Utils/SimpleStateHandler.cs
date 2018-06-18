using Neco.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.UnitTest
{
    class SimpleStateHandler : StateHandler
    {
        public void SetState(State state)
        {
            CurrentState = state;
        }
    }
}
