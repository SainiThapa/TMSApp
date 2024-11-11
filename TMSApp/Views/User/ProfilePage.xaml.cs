using Xamarin.Forms;
using TMSApp.Services;
using TMSApp.ViewModels;
using System.Windows.Input;

namespace TMSApp.Views.User
{
    public partial class ProfilePage : ContentPage
    {
        private readonly AuthService _service;

        public UserProfileViewModel Profile { get; set; }

        public ProfilePage()
        {
            InitializeComponent();
            _service = new AuthService();
            LoadUserProfileAsync();
        }

        private async void LoadUserProfileAsync()
        {
            Profile = await _service.GetUserProfileAsync();

            if (Profile != null)
            {
                BindingContext = Profile;
            }
            else
            {
                await DisplayAlert("Error", "Failed to load profile.", "OK");
            }
        }
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new UserHomePage();
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {

        }
    }
}
