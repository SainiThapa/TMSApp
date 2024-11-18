using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TMSApp.Services;
using Xamarin.Forms;

namespace TMSApp.ViewModels
{
    public class AdminUserListViewModel : BaseViewModel
    {
        private readonly AdminService _adminService;

        public ObservableCollection<UserViewModel> Users { get; set; }

        public Command LoadUsersCommand { get; }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public AdminUserListViewModel()
        {
            _adminService = new AdminService();
            Users = new ObservableCollection<UserViewModel>();
            LoadUsersCommand = new Command(async () => await LoadUsersAsync());
        }

        private async Task LoadUsersAsync()
        {
            IsBusy = true;

            var userList = await _adminService.GetUserListAsync();
            if (userList != null)
            {
                Users.Clear();
                foreach (var user in userList)
                {
                    Users.Add(user);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load users.", "OK");
            }

            IsBusy = false;
        }
    }
}
