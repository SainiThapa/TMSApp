using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void OnEditClicked(object sender,EventArgs e)
        {
                //await Navigation.PushAsync(new EditTaskPage());
        }
    }

}