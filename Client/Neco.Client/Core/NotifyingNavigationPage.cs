using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Neco.Client
{
    public class NotifyingNavigationPage : NavigationPage
    {
        private NotifiableContentPage lastPage;

        public NotifyingNavigationPage() : base()
        {
            SetupEvents();
        }

        public NotifyingNavigationPage(Page page) : base(page)
        {
            SetupEvents();
        }

        private void SetupEvents()
        {
            Popped += (object sender, NavigationEventArgs e) =>
            {
                if(e.Page is NotifiableContentPage)
                {
                    lastPage = (e.Page as NotifiableContentPage);
                    lastPage.OnPopped();
                }
            };
        }
    }
}
