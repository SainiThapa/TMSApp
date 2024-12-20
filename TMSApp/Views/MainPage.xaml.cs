﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSApp.Views;
using Xamarin.Forms;

namespace TMSApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void LoginUser_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new UserLoginPage());
            await Navigation.PushAsync(new UserLoginPage());

        }
        private async void RegisterAccount_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new RegisterAccount());
            await Navigation.PushAsync(new RegisterAccount());
        }

    }
}
