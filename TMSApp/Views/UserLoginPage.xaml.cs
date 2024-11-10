using System;
using TMSApp.ViewModels;
using Xamarin.Forms;
using TMSApp.Services;
using TMSApp.Views.User;
using Xamarin.CommunityToolkit.Extensions;

namespace TMSApp.Views
{
    public partial class UserLoginPage : ContentPage
    {
        private UserLoginPageViewModel viewModel;

        public UserLoginPage()
        {
            InitializeComponent();
            viewModel = new UserLoginPageViewModel();
            BindingContext = viewModel;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            if (!viewModel.IsEmailErrorVisible && !viewModel.IsPasswordErrorVisible)
            {
                string email = viewModel.Email;
                string password = viewModel.Password;

                AuthService service = new AuthService();
                
                var response = await service.LoginAsync(email, password);

                if (response)
                {
                    await this.DisplayToastAsync("Login Successful", 1000);
                    Application.Current.MainPage = new UserHomePage();
                }
                //await DisplayAlert("Login", $"Email: {email}\nPassword: {password}", "OK");
            }
            else
            {
                await DisplayAlert("Validation Error", "Please fix the errors before logging in.", "OK");
            }
        }
    }
}
