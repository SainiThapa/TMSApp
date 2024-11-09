using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMSApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomePageFlyout : ContentPage
    {
        public ListView ListView;

        public UserHomePageFlyout()
        {
            InitializeComponent();

            BindingContext = new UserHomePageFlyoutViewModel();
            ListView = MenuItemsListView;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as UserHomePageFlyoutMenuItem;
                if (item == null)
                {
                    Debug.WriteLine("Selected item is null");
                    return;
                }

                if (item.Id == 2)
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage()); // Redirect to MainPage
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