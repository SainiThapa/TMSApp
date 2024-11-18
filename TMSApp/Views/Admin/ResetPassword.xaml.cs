using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMSApp.ViewModels;
using System;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassword : ContentPage
    {
        public ResetPassword(string email)
        {
            InitializeComponent();
            BindingContext = new ResetPasswordViewModel(email);
        }
    }
}
