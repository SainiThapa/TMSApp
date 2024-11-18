using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSApp.Services;
using Xamarin.Essentials;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : FlyoutPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AdminHomePageFlyoutMenuItem;
            if (item == null)
                return;
            if (item.Id == 0)
            {
                //Navigation.PushAsync(new AdminHomePage());
                Application.Current.MainPage = new NavigationPage(new AdminHomePage());
            }
            else if (item.Id == 1)
            {

                var token = await SecureStorage.GetAsync("jwt_token");

                if (!string.IsNullOrEmpty(token))
                {
                    // Decode the JWT token to extract the email claim
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jwtToken != null)
                    {
                        var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;

                        if (!string.IsNullOrEmpty(email))
                        {
                            await Navigation.PushAsync(new ResetPassword(email));
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Email claim not found in token.", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Invalid token format.", "OK");
                    }
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "No token found. Please log in again.", "OK");
            }
            else if (item.Id == 2)
            {
                await Navigation.PushAsync(new UserListPage());
                //Application.Current.MainPage = new NavigationPage(new UserListPage());
            }
            else if (item.Id == 3)
            {
                await Navigation.PushAsync(new ReportSummaryPage());
                //Application.Current.MainPage = new NavigationPage(new ReportSummaryPage());
            }
            else if (item.Id == 4)
            {
                SecureStorage.Remove("jwt_token");
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}