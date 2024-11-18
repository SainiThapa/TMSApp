using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMSApp.ViewModels.Admin;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportSummaryPage : ContentPage
    {
        private ReportSummaryViewModel _viewModel;

        public ReportSummaryPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ReportSummaryViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.InitializeAsync();
        }
    }
}
