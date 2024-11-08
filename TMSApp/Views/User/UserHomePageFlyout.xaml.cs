using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMSApp.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomePageFlyout : ContentPage
    {
        public ListView ListView;

        public UserHomePageFlyout()
        {
            InitializeComponent();

            BindingContext = new UserHomePageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class UserHomePageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserHomePageFlyoutMenuItem> MenuItems { get; set; }

            public UserHomePageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<UserHomePageFlyoutMenuItem>(new[]
                {
                    new UserHomePageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new UserHomePageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new UserHomePageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new UserHomePageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new UserHomePageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}