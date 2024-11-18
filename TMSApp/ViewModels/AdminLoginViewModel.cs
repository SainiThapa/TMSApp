using System;
using System.Windows.Input;
using Xamarin.Forms;
using TMSApp.Services;
using System.Threading.Tasks;
using TMSApp.Views.Admin;

namespace TMSApp.ViewModels
{
    public class AdminLoginViewModel : BaseViewModel
    {
        private readonly AdminService _adminService;

        public AdminLoginViewModel()
        {
            _adminService = new AdminService();
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set => SetProperty(ref _emailError, value);
        }

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }

        private bool _isEmailErrorVisible;
        public bool IsEmailErrorVisible
        {
            get => _isEmailErrorVisible;
            set => SetProperty(ref _isEmailErrorVisible, value);
        }

        private bool _isPasswordErrorVisible;
        public bool IsPasswordErrorVisible
        {
            get => _isPasswordErrorVisible;
            set => SetProperty(ref _isPasswordErrorVisible, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand LoginCommand { get; }

        public async Task<bool> LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email is required.";
                IsEmailErrorVisible = true;
                return false;
            }
            else
            {
                IsEmailErrorVisible = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required.";
                IsPasswordErrorVisible = true;
                return false;
            }
            else
            {
                IsPasswordErrorVisible = false;
            }

            IsBusy = true;

            var isSuccess = await _adminService.AdminLoginAsync(Email, Password);

            if (isSuccess)
            {
                // Navigate to admin-specific page
                await Application.Current.MainPage.Navigation.PushAsync(new AdminHomePage());
                IsBusy = false;
                return true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid admin credentials.", "OK");
                IsBusy = false;
                return false;
            }
        }
    }
}
