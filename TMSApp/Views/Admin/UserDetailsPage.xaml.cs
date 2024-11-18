using Xamarin.Forms;
using TMSApp.ViewModels;
using TMSApp.Services;
using System.Linq;
using System;

namespace TMSApp.Views.Admin
{
    public partial class UserDetailsPage : ContentPage
    {
        private readonly AdminService _adminService;

        public UserDetailsViewModel ViewModel { get; set; }

        public UserDetailsPage(string userId)
        {
            InitializeComponent();
            _adminService = new AdminService();
            LoadUserDetails(userId);
        }

        private async void LoadUserDetails(string userId)
        {
            var userDetails = await _adminService.GetUserDetailsAsync(userId);
            if (userDetails != null)
            {
                ViewModel = userDetails;
                ViewModel.UserId = userId;
                BindingContext = ViewModel;
                foreach (var task in ViewModel.Tasks)
                {
                    Console.WriteLine($"Task Loaded: {task.Id}, Title: {task.Title}");
                }
            }
            else
            {
                await DisplayAlert("Error", "Failed to load user details.", "OK");
            }
        }

        private async void UsersListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is UserViewModel selectedUser)
            {
                await Navigation.PushAsync(new UserDetailsPage(selectedUser.Id));
            }
        }

        private void OnTaskSelected(object sender, CheckedChangedEventArgs e)
        {

            if (sender is CheckBox checkbox && checkbox.BindingContext is UserTaskViewModel task)
            {
                if (checkbox.IsChecked)
                {

                    if (!ViewModel.SelectedTasks.Contains(task))
                    {
                        ViewModel.SelectedTasks.Add(task);
                    }
                }
                else
                {
                    if (ViewModel.SelectedTasks.Contains(task))
                    {
                        ViewModel.SelectedTasks.Remove(task);
                    }
                }
                Console.WriteLine($"Task Selected: {task.Id}, IsChecked: {checkbox.IsChecked}");
                Console.WriteLine($"Selected Tasks: {string.Join(", ", ViewModel.SelectedTasks.Select(t => t.Id))}");
            }
        }

        private async void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is UserViewModel user)
            {
                await Navigation.PushAsync(new UserDetailsPage(user.Id));
            }
        }
        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            if (BindingContext is UserDetailsViewModel viewModel)
            {
                string userEmail = viewModel.Email; // Get the selected user's email
                await Navigation.PushAsync(new ResetPassword(userEmail));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not found.", "OK");
            }
        }
    }
}
