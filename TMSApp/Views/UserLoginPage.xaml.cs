using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSApp.Views.User;    
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserLoginPage : ContentPage
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserHomePage());
            //DisplayAlert("Login", "Login successful", "OK");
        }

    }
}