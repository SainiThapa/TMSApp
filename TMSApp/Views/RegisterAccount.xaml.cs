using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAccount : ContentPage
    {
        public RegisterAccount()
        {
            InitializeComponent();
        }

        private void Button_Register(object sender, EventArgs e)
        {
            DisplayAlert("Registration","Successful registration !", "OK");
        }
    }
}