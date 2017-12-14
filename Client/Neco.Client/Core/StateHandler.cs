using System;
using System.Collections.Generic;
using System.Text;

namespace Neco.Client.Core
{
    public enum State
    {
        Error,
        Unknown,
        Success,
    }

    public class StateHandler
    {
        private State state = State.Unknown;
        private object lockObj = new object();

        private EventHandler stateChanged;
        public event EventHandler StateChanged
        {
            add
            {
                lock (lockObj)
                {
                    stateChanged -= value;
                    stateChanged += value;
                    value?.Invoke(this, null);
                }
            }
            remove
            {
                lock (lockObj)
                {
                    stateChanged -= value;
                }
            }
        }

        public State CurrentState
        {
            get
            {
                lock (lockObj)
                {
                    return state;
                }
            }
            protected set
            {
                lock (lockObj)
                {
                    if (state != value)
                    {
                        state = value;
                        //if (state != State.Unknown)
                        {
                            stateChanged?.Invoke(this, null);
                        }
                    }
                }
            }
        }
    }
}
