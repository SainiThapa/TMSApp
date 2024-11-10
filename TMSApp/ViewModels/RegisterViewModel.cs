using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TMSApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string email;
        private string password;
        private string confirmPassword;
        private string firstname;
        private string lastname;

        public event PropertyChangedEventHandler PropertyChanged;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
                IsEmailErrorVisible = string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email);
            }
        }

        public bool IsEmailErrorVisible { get; private set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                IsPasswordErrorVisible = string.IsNullOrWhiteSpace(password) || password.Length < 6;
                IsConfirmPasswordErrorVisible = password != ConfirmPassword;
            }
        }

        public bool IsPasswordErrorVisible { get; private set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
                IsConfirmPasswordErrorVisible = confirmPassword != Password;
            }
        }

        public bool IsConfirmPasswordErrorVisible { get; private set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FirstName
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged();
                IsNameError = string.IsNullOrEmpty(firstname)||string.IsNullOrEmpty(LastName);
            }
        }
        public string LastName
        {
            get => lastname;
            set
            {
                lastname = value;
                OnPropertyChanged();
                IsNameError = string.IsNullOrEmpty(lastname)||string.IsNullOrEmpty(FirstName);
            }
        }
        public bool IsNameError { get; private set; }

    }
}
