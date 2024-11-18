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
        new AdminHomePageFlyoutMenuItem(typeof(AdminHomePage),0,"To Do List"),
        new AdminHomePageFlyoutMenuItem(typeof(ResetPassword), 1, "Reset Password"),
        new AdminHomePageFlyoutMenuItem(typeof(UserListPage),2,"User List"),
        new AdminHomePageFlyoutMenuItem(typeof(ReportSummaryPage),3,"Reports and Summary"),
        new AdminHomePageFlyoutMenuItem(typeof(MainPage), 4, "Logout"),

    });
        }
    }
}