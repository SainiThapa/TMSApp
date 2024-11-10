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
    public partial class CreateTaskPage : ContentPage
    {
        public CreateTaskPage()
        {
            InitializeComponent();
            BindingContext = new CreateTaskViewModel();
        }
    }
}