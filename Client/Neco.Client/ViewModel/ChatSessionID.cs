using System;
using System.Collections.Generic;
using System.Text;

namespace Neco.Client.ViewModel
{
    public class ChatSessionID : ViewModelBase
    {
        private string sessionID;
        public string SessionID
        {
            get { return sessionID; }
            set { SetProperty(ref sessionID, value); }
        }
    }
}
