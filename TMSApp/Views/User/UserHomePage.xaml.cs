using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (item.Id == 2)
            {
                // Implement logout logic
                Application.Current.MainPage = new NavigationPage(new MainPage());
                Navigation.RemovePage(this);
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}