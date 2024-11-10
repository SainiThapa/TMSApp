using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomePage : FlyoutPage
    {
        public UserHomePage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as UserHomePageFlyoutMenuItem;
            if (item == null)
                return;
            if (item.Id == 0)
            {
                Application.Current.MainPage = new UserHomePage();
            }
            else if (item.Id == 1)
            {
                SecureStorage.Remove("jwt_token");
                Application.Current.MainPage = new MainPage();
            }
        }
    }
}