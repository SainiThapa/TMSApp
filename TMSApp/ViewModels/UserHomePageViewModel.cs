using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace TMSApp.ViewModels
{
    internal class UserFlyoutHomePageViewModel
    {
        public ObservableCollection<FlyoutItem> MenuItems { get; set; }

        public UserFlyoutHomePageViewModel()
        {
            MenuItems = new ObservableCollection<FlyoutItem>
        {
            new FlyoutItem { Title = "Reset Password" },
            new FlyoutItem { Title = "Logout" }
        };
        }
    }
    public class FlyoutItem
    {
        public string Title { get; set; }
    }
}
