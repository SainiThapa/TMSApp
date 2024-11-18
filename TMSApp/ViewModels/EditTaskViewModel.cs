using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TMSApp.Models;
using TMSApp.Views.User;
using TMSApp.Services;
using Xamarin.Forms;
using TMSApp.Views.Admin;
using Xamarin.Essentials;

namespace TMSApp.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
    {
        private readonly TaskService _taskService;
        private readonly INavigation _navigation;

        public TaskViewModel Task { get; set; }

        public ICommand SaveCommand { get; }

        public EditTaskViewModel(TaskViewModel task, INavigation navigation)
        {
            Task = task;
            _taskService = new TaskService();

            SaveCommand = new Command(async () => await SaveTaskAsync());
            _navigation = navigation;
        }

        private async Task SaveTaskAsync()
        {
            var isSuccess = await _taskService.UpdateTaskAsync(Task);
            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Task updated successfully!", "OK");
                try
                {
                    await _navigation.PopAsync();

                    //Application.Current.MainPage = new UserHomePage();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\n==========Exception======="+ e.Message);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update the task.", "OK");
            }
        }
    }
}
