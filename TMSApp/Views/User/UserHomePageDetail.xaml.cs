using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using TMSApp.Models;
using TMSApp.Services;
using TMSApp.ViewModels;
using TMSApp.Views.Tasks;

namespace TMSApp.Views.User
{
    public partial class UserHomePageDetail : ContentPage
    {
        private readonly TaskService _taskService;
        public ObservableCollection<TaskViewModel> Tasks { get; set; }
        public int TaskCount => Tasks?.Count ?? 0;

        public Command<TaskViewModel> EditTaskCommand { get; }
        public Command<TaskViewModel> DeleteTaskCommand { get; }
        private bool _isNavigating = false;

        public UserHomePageDetail()
        {
            InitializeComponent();

            _taskService = new TaskService();
            Tasks = new ObservableCollection<TaskViewModel>();

            BindingContext = this;
            EditTaskCommand = new Command<TaskViewModel>(async (task) => await EditTaskCommand_Executed(task));
            DeleteTaskCommand = new Command<TaskViewModel>(async (task) => await DeleteTaskCommand_Executed(task));

            LoadTasksAsync();
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                // Fetch tasks from the API using TaskService
                var tasks = await _taskService.GetTaskAsync();
                if (tasks != null)
                {
                    Tasks.Clear();
                    foreach (var task in tasks)
                    {
                        Tasks.Add(task);
                    }

                    // Update the task count
                    OnPropertyChanged(nameof(TaskCount));
                }
                else
                {
                    await DisplayAlert("Error", "Failed to load tasks.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await DisplayAlert("Error", "An error occurred while loading tasks.", "OK");
            }
        }
        private async Task EditTaskCommand_Executed(TaskViewModel task)
        {
            await Navigation.PushAsync(new EditTaskPage(task));
        }

        private async Task DeleteTaskCommand_Executed(TaskViewModel task)
        {
            var confirm = await DisplayAlert("Delete", $"Are you sure you want to delete '{task.Title}'?", "Yes", "No");
            if (confirm)
            {
                var result = await _taskService.DeleteTaskAsync(task.Id);
                if (result)
                {
                    await DisplayAlert("Deleted", "Task Deleted Succesfully", "OK");
                    Tasks.Remove(task);
                    OnPropertyChanged(nameof(TaskCount));
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete the task.", "OK");
                }
            }
            Console.WriteLine("DENIEDD................");
        }

        // Refresh tasks when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTasksAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (_isNavigating) return;

            try
            {
                _isNavigating = true;
                Navigation.PushAsync(new CreateTaskPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex.Message}");
                DisplayAlert("Error", "Unable to open the Add Task page.", "OK");
            }
            finally
            {
                _isNavigating = false;
            }
        }
    }
}
