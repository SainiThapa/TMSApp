using System;
using TMSApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMSApp.Views.User;
using TMSApp.Views.Admin;
using TMSApp.Views.Tasks;


namespace TMSApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            //Application.Current.MainPage = new FlyoutPage
            //{
            //    Flyout = new AdminHomePageFlyout(),
            //    Detail = new NavigationPage(new AdminHomePageDetail())
            //};
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
