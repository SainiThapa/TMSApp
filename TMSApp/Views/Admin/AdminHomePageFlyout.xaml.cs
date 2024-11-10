using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMSApp.ViewModels;
using System.Diagnostics;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePageFlyout : ContentPage
    {
        public ListView ListView { get { return MenuItemsListView; } }

        public AdminHomePageFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminHomePageFlyoutViewModel();
            MenuItemsListView.ItemsSource = ((AdminHomePageFlyoutViewModel)BindingContext).MenuItems;
            MenuItemsListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as AdminHomePageFlyoutMenuItem;
                if (item == null)
                {
                    Debug.WriteLine("Selected item is null");
                    return;
                }

                if (item.Id == 3) // Assuming '3' is the ID for "Logout"
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage()); // Redirect to LoginPage
                }
                else
                {

                Debug.WriteLine("Creating instance of: " + item.TargetType.FullName);
                var page = (Page)Activator.CreateInstance(item.TargetType);
                if (page == null)
                {
                    Debug.WriteLine("Page instance is null");
                    return;
                }

                Debug.WriteLine("Navigating to page: " + page.Title);
                var mainPage = Application.Current.MainPage as FlyoutPage;
                if (mainPage == null)
                {
                    Debug.WriteLine("MainPage is not a FlyoutPage");
                    return;
                }

                mainPage.Detail = new NavigationPage(page);
                mainPage.IsPresented = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in ListView_ItemSelected: " + ex);
            }
            finally
            {
                ((ListView)sender).SelectedItem = null;  // Deselect the item
            }
        }
    }
}