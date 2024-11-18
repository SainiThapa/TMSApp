using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using TMSApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePageFlyout : ContentPage
    {
        public ListView ListView;

        public AdminHomePageFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminHomePageFlyoutViewModel();
            ListView = MenuItemsListView;
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

                if (item.Id == 4)
                {
                    SecureStorage.Remove("jwt_token");
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