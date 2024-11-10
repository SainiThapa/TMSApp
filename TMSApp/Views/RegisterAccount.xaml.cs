using System;
using Xamarin.Forms;
using TMSApp.ViewModels;
using System.Threading.Tasks;
using TMSApp.Services;
using Xamarin.Essentials;
using TMSApp.Views.User;
using Xamarin.CommunityToolkit.Extensions;

namespace TMSApp.Views
{
    public partial class RegisterAccount : ContentPage
    {
        private RegisterViewModel viewModel;
        public RegisterAccount()
        {
            InitializeComponent();
            viewModel = new RegisterViewModel();
            BindingContext = viewModel;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Check if there are any visible errors
            if (viewModel.IsEmailErrorVisible || viewModel.IsPasswordErrorVisible
                || viewModel.IsConfirmPasswordErrorVisible || viewModel.IsNameError)
            {
                await DisplayAlert("Error", "Please correct the errors before submitting.", "OK");
                return;
            }

            AuthService service = new AuthService();
            bool isRegistered = await service.RegisterAsync(viewModel.Email, viewModel.Password, viewModel.FirstName, viewModel.LastName);

            if (isRegistered)
            {
                await DisplayAlert("Success", "Registration successful!", "OK");
                var response = await service.LoginAsync(viewModel.Email, viewModel.Password);

                if (response)
                {
                    await this.DisplayToastAsync("Login Successful", 1000);
                    Application.Current.MainPage = new UserHomePage();
                }
            }
            else
            {
                await DisplayAlert("Error", "Registration failed. Please try again.", "OK");
            }
        }
    }
}