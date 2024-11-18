using System;
using Xamarin.Forms;
using TMSApp.ViewModels;
using TMSApp.Views.Admin;

namespace TMSApp.Views
{
    public partial class AdminLoginPage : ContentPage
    {
        private readonly AdminLoginViewModel _viewModel;

        public AdminLoginPage()
        {
            InitializeComponent();
            _viewModel = new AdminLoginViewModel();
            BindingContext = _viewModel;
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            // Perform validation before login
            if (string.IsNullOrWhiteSpace(_viewModel.Email))
            {
                _viewModel.EmailError = "Email is required.";
                _viewModel.IsEmailErrorVisible = true;
                return;
            }

            _viewModel.IsEmailErrorVisible = false;

            if (string.IsNullOrWhiteSpace(_viewModel.Password))
            {
                _viewModel.PasswordError = "Password is required.";
                _viewModel.IsPasswordErrorVisible = true;
                return;
            }

            _viewModel.IsPasswordErrorVisible = false;

            // Call the login method
            bool isLoginSuccessful = await _viewModel.LoginAsync();

            if (isLoginSuccessful)
            {
                await DisplayAlert("Success", "Welcome Admin!", "OK");
                // Navigate to the Admin Dashboard or Home Page
                    Application.Current.MainPage = new NavigationPage(new AdminHomePage());
                //await Navigation.PushAsync(new AdminHomePage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid admin credentials.", "OK");
            }
        }
    }
}
