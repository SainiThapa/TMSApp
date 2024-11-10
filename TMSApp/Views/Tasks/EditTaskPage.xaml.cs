using System;
using TMSApp.Models;
using TMSApp.ViewModels;
using Xamarin.Forms;

namespace TMSApp.Views.Tasks
{
    public partial class EditTaskPage : ContentPage
    {
        public EditTaskPage(TaskViewModel task)
        {
            InitializeComponent();

            BindingContext = new EditTaskViewModel(task);
        }
    }
}
