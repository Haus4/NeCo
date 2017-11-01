using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Neco.Client
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            this.chatButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new Chat());
            };
        }
	}
}
