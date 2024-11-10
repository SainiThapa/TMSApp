using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using TMSApp.Views.User;
using TMSApp.Models;
using TMSApp.Services;

namespace TMSApp.ViewModels
{
    public class CreateTaskViewModel : BaseViewModel
    {
        private readonly TaskService _taskService;

        // Properties for binding to the form fields
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDueDate { get; set; } = DateTime.Today;
        public bool IsActive { get; set; } = true;
        public ICommand SaveTaskCommand { get; }

        public CreateTaskViewModel()
        {
            _taskService = new TaskService();
            SaveTaskCommand = new Command(async () => await SaveTaskAsync());
        }

        private async Task SaveTaskAsync()
        {
            // Collect the form data into a new TaskViewModel instance
            var newTask = new TaskViewModel
            {
                Title = TaskTitle,
                Description = TaskDescription,
                DueDate = TaskDueDate,
                IsActive = IsActive
            };

            // Call AddTaskAsync to send the data to the API
            bool isAdded = await _taskService.AddTaskAsync(newTask);
            if (isAdded)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Task added successfully.", "OK");
                Application.Current.MainPage = new UserHomePage();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add task.", "OK");
            }
        }
    }
}
