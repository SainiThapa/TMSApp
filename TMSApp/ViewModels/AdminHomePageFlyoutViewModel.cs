using System;
using System.Collections.Generic;
using System.Text;
using TMSApp.Views.Admin;
using TMSApp.Views.Tasks;
using System.Collections.ObjectModel;
using TMSApp.Models;

namespace TMSApp.ViewModels
{
    public class AdminHomePageFlyoutViewModel
    {
        public ObservableCollection<AdminHomePageFlyoutMenuItem> MenuItems { get; set; }

        public AdminHomePageFlyoutViewModel()
        {
            MenuItems = new ObservableCollection<AdminHomePageFlyoutMenuItem>(new[]
        {
        new AdminHomePageFlyoutMenuItem(typeof(TaskItems),0,"To Do List"),
        new AdminHomePageFlyoutMenuItem(typeof(UserList), 1, "List of all Users"),
        new AdminHomePageFlyoutMenuItem(typeof(MainPage), 2, "Logout"),  // Assuming you redirect to LoginPage on logout
        
    }); 
        }
    }
}