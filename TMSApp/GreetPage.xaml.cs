using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreetPage : ContentPage
    {

        Button greetButton;
        StackLayout layout =  new StackLayout();
        public GreetPage()
        {
            InitializeComponent();
            greetButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Welcome to TMS"
            };
            greetButton.Clicked += GreetButton_Clicked;
            layout.Children.Add(greetButton);
            Content = layout;
        }

        private void GreetButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Greet","Hello, welcome to TMS","OK");
        }
    }
}