using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TMSApp.Views.Accounts;

namespace TMSApp.ViewModels
{
public class FlyoutMenuViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ICommand MenuItemCommand { get; private set; }

        public FlyoutMenuViewModel()
        {
            MenuItemCommand = new Command<MenuItem>(OnMenuItemSelected);
            MenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem { Id = 1, Title = "Change Password" },
            new MenuItem { Id = 2, Title = "Logout" }
        };
        }

        private async void OnMenuItemSelected(MenuItem item)
        {
            if (item == null)
                return;

            switch (item.Id)
            {
                case 1:
                    await Application.Current.MainPage.Navigation.PushAsync(new ChangePassword());
                    break;
                case 2:
                    // Add logout logic here
                    break;
            }
        }
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

}
