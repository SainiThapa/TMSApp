using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : FlyoutPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AdminHomePageFlyoutMenuItem;
            if (item == null)
                return;
            if (item.Id == 3)  
            {
                // Implement logout logic
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                Page page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
            }
            IsPresented = false;

            ((ListView)sender).SelectedItem = null;
        }
    }
}