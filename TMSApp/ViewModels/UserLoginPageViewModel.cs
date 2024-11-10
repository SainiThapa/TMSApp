using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace TMSApp.ViewModels
{
    public class UserLoginPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _email;
        private string _password;
        private string _emailError;
        private string _passwordError;
        private bool _isEmailErrorVisible;
        private bool _isPasswordErrorVisible;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ValidateEmail();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidatePassword();
            }
        }

        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChanged();
            }
        }

        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmailErrorVisible
        {
            get => _isEmailErrorVisible;
            set
            {
                _isEmailErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsPasswordErrorVisible
        {
            get => _isPasswordErrorVisible;
            set
            {
                _isPasswordErrorVisible = value;
                OnPropertyChanged();
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Please enter a valid email address.";
                IsEmailErrorVisible = true;
            }
            else
            {
                IsEmailErrorVisible = false;
            }
        }

        private void ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 8)
            {
                PasswordError = "Password must be at least 8 characters long.";
                IsPasswordErrorVisible = true;
            }
            else
            {
                IsPasswordErrorVisible = false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
