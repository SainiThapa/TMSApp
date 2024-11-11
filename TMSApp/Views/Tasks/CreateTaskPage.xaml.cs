using System;
using TMSApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTaskPage : ContentPage
    {
        public CreateTaskPage()
        {
            InitializeComponent();
            DueDatePicker.MinimumDate = DateTime.Today;
            BindingContext = new CreateTaskViewModel();
        }
    }
}