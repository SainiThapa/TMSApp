using Xamarin.Forms;
using TMSApp.ViewModels;

namespace TMSApp.Views.Admin
{
    public partial class UserListPage : ContentPage
    {
        private readonly AdminUserListViewModel _viewModel;

        public UserListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AdminUserListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel.Users.Count == 0)
                _viewModel.LoadUsersCommand.Execute(null);
        }
        private async void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is UserViewModel selectedUser)
            {
                await Navigation.PushAsync(new UserDetailsPage(selectedUser.Id));
            }
            ((ListView)sender).SelectedItem = null;
        }

    }
}
