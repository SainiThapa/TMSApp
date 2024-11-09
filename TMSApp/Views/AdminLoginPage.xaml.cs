using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMSApp.Views.Admin;

namespace TMSApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminLoginPage : ContentPage
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        private async void AdminLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminHomePage());
            Navigation.RemovePage(this);
            //DisplayAlert("Login", "Login success", "OK");
        }
    }
}