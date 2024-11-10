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

        public Command AddTaskCommand { get; }
        public Command<TaskViewModel> EditTaskCommand { get; }
        public Command<TaskViewModel> DeleteTaskCommand { get; }
        public UserHomePageDetail()
        {
            InitializeComponent();

            // Initialize TaskService and ObservableCollection
            _taskService = new TaskService();
            Tasks = new ObservableCollection<TaskViewModel>();

            // Set BindingContext for XAML data binding
            BindingContext = this;
            AddTaskCommand = new Command(async () => await AddTaskCommand_Executed());
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


        private async Task AddTaskCommand_Executed()
        {
            await Navigation.PushAsync(new CreateTaskPage());
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
                    Tasks.Remove(task);
                    OnPropertyChanged(nameof(TaskCount));
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete the task.", "OK");
                }
            }
        }

        // Refresh tasks when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTasksAsync();
        }
    }
}
