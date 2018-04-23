using System;
using Xamarin.Forms;

namespace Neco.Client.ViewModel
{
    public class ChatMessage : ViewModelBase
    {
        private string message;
        private DateTime time;
        private bool isForeign;
        private bool isImage;
        private ImageSource image;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        public DateTime Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
        public bool IsForeign
        {
            get { return isForeign; }
            set { SetProperty(ref isForeign, value); }
        }

        public bool IsImage
        {
            get { return isImage; }
            set { SetProperty(ref isImage, value); }
        }

        public string StringTime
        {
            get
            {
                return Time.ToShortTimeString();
            }
        }

        public ImageSource Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }
    }
}
