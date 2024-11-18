using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TMSApp.Services;
using TMSApp.ViewModels;
using Xamarin.Forms;

public class UserDetailsViewModel : BaseViewModel
{
    private readonly AdminService _adminService;

    public ObservableCollection<UserTaskViewModel> Tasks { get; set; }
    public ObservableCollection<UserTaskViewModel> SelectedTasks { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; }
    public ICommand ResetPasswordCommand { get; }

    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserDetailsViewModel()
    {
        _adminService = new AdminService();
        Tasks = new ObservableCollection<UserTaskViewModel>();
        SelectedTasks = new ObservableCollection<UserTaskViewModel>();

        DeleteSelectedTasksCommand = new Command(DeleteSelectedTasks);
        ResetPasswordCommand = new Command(ResetPassword);
    }

    private async void DeleteSelectedTasks()
    {
        if (!SelectedTasks.Any())
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No tasks selected.", "OK");
            return;
        }
        var taskIdsToDelete = SelectedTasks.Select(t => t.Id).ToList();
        Console.WriteLine($"Task IDs to delete: {string.Join(", ", taskIdsToDelete)}");

        bool isDeleted = await _adminService.DeleteTasksAsync(UserId, taskIdsToDelete);

        if (isDeleted)
        {
            foreach (var task in SelectedTasks.ToList())
            {
                Tasks.Remove(task);
            }
                SelectedTasks.Clear();
            await Application.Current.MainPage.DisplayAlert("Success", "Selected tasks deleted.", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to delete selected tasks.", "OK");
        }
    }

    private async void ResetPassword()
    {
        string newPassword = await Application.Current.MainPage.DisplayPromptAsync(
            "Reset Password",
            "Enter the new password for the user:",
            "OK",
            "Cancel",
            placeholder: "New Password",
            maxLength: 50,
            keyboard: Keyboard.Text);

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Password cannot be empty.", "OK");
            return;
        }

        // Call the service to reset the password
        bool isReset = await _adminService.ResetUserPasswordAsync(Email, newPassword);

        if (isReset)
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Password reset successfully.", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("System Error", "Failed to reset password.", "OK");
        }
    }
}
