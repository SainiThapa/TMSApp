﻿using System;
using System.Collections.Generic;
using System.Text;
using TMSApp.Views.User;
using TMSApp.Views.Tasks;
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
        new UserHomePageFlyoutMenuItem(typeof(ProfilePage), 0, "My Profile"),
        new UserHomePageFlyoutMenuItem(typeof(UserHomePage),1,"To Do List"),
        new UserHomePageFlyoutMenuItem(typeof(MainPage), 2, "Logout"),

    });
        }
    }
}