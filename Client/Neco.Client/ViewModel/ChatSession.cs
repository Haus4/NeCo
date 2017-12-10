using System;
using System.Collections.Generic;
using System.Text;

namespace Neco.Client.ViewModel
{
    public class ChatSession : ViewModelBase
    {
        private int sessionNum;
        private byte[] remotePublicKey;
        public byte[] RemotePublicKey
        {
            get { return remotePublicKey; }
            set { SetProperty(ref remotePublicKey, value); }
        }
        public int SessionNum
        {
            get { return sessionNum; }
            set { SetProperty(ref sessionNum, value); }
        }
    }
}
