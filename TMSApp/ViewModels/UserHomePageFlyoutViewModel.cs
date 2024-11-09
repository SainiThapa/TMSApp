using System;
using System.Collections.Generic;
using System.Text;
using TMSApp.Views.User;
using TMSApp.Views.Tasks;
using TMSApp.Views.Accounts;
using System.Collections.ObjectModel;
using TMSApp.Models;


namespace TMSApp.ViewModels
{
    public class UserHomePageFlyoutViewModel
    {
        public ObservableCollection<UserHomePageFlyoutMenuItem> MenuItems { get; set; }

        public UserHomePageFlyoutViewModel()
        {
            MenuItems = new ObservableCollection<UserHomePageFlyoutMenuItem>(new[]
        {
        new UserHomePageFlyoutMenuItem(typeof(TaskItems),0,"To Do List"),
        new UserHomePageFlyoutMenuItem(typeof(ChangePassword), 1, "Reset your Password"),
        new UserHomePageFlyoutMenuItem(typeof(MainPage), 2, "Logout"),
    });
        }
    }
}