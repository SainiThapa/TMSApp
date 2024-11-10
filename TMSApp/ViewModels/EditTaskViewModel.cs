using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TMSApp.Models;
using TMSApp.Services;
using Xamarin.Forms;

namespace TMSApp.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
    {
        private readonly TaskService _taskService;
        public TaskViewModel Task { get; set; }

        public ICommand SaveCommand { get; }

        public EditTaskViewModel(TaskViewModel task)
        {
            Task = task;
            _taskService = new TaskService();

            SaveCommand = new Command(async () => await SaveTaskAsync());
        }

        private async Task SaveTaskAsync()
        {
            var isSuccess = await _taskService.UpdateTaskAsync(Task);
            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Task updated successfully!", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update the task.", "OK");
            }
        }
    }
}
