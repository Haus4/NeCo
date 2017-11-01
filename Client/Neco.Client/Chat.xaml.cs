using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
    public partial class Chat : ContentPage
    {
        public Chat()
        {
            InitializeComponent();

            this.Appearing += (sender, args) =>
            {
                this.textArea.Focus();
            };
        }
    }
}
