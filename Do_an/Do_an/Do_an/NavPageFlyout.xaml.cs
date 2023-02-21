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

namespace Do_an
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavPageFlyout : ContentPage
    {
        public ListView ListView;
        public User u;

        public NavPageFlyout()
        {
            InitializeComponent();
            u = User.loggedInUser;
            user.BindingContext = u;

            BindingContext = new NavPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class NavPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavPageFlyoutMenuItem> MenuItems { get; set; }

            public NavPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<NavPageFlyoutMenuItem>(new[]
                {
                    new NavPageFlyoutMenuItem { Id = 0, Title = "Trang chủ", TargetType = typeof(HomePage)},
                    new NavPageFlyoutMenuItem { Id = 1, Title = "Đơn hàng của tôi", TargetType = typeof(OrderMgmtPage) }
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProfilePage());
        }
    }
}