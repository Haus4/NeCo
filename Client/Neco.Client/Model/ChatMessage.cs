using System;

namespace Neco.Client
{
    public class ChatMessage
    {
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public bool IsForeign { get; set; }

        public string StringTime
        {
            get
            {
                return Time.ToShortTimeString();
            }
        }
    }
}
