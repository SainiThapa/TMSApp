using System.Collections.Generic;
using System;
using Xamarin.Forms;
using TMSApp.ViewModels;
namespace TMSApp.Views.Tasks
{

    public partial class TaskItems : ContentPage
    {
        public TaskItems()
        {
            InitializeComponent();
            // Assume TaskViewModel has a list of tasks
            tasksListView.ItemsSource = new List<TaskViewModel>
        {
            new TaskViewModel { Title = "Task 1", Description = "Description 1" },
            new TaskViewModel { Title = "Task 2", Description = "Description 2" },
            // Add more demo tasks here
        };
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var task = e.SelectedItem as TaskViewModel;
            if (task == null)
                return;

            // Navigate to TaskDetails page
            Navigation.PushAsync(new TaskDetailPage());
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button?.CommandParameter as TaskViewModel;
            if (task != null)
            {
                // Handle edit logic or navigate to edit page
                //Navigation.PushAsync(new TaskEditPage());
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var task = button?.CommandParameter as TaskViewModel;
            if (task != null)
            {
                // Handle delete logic
                // For example: Remove task from the list
            }
        }
    }
}