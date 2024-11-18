using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xamarin.Forms;
using TMSApp.Services;

namespace TMSApp.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly AdminService _adminService;

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        [Required(ErrorMessage = "Password is required.")]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public Command ResetPasswordCommand { get; }

        public ResetPasswordViewModel(string email)
        {
            _adminService = new AdminService();
            Email = email;
            ResetPasswordCommand = new Command(async () => await ResetPasswordAsync());
        }

        private async Task ResetPasswordAsync()
        {
            if (string.IsNullOrWhiteSpace(Password) || Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password validation failed.", "OK");
                return;
            }

            var isSuccess = await _adminService.ResetUserPasswordAsync(Email, Password);
            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Password updated successfully.", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error Occured!", "Failed to update password.", "OK");
            }
        }
    }
}
