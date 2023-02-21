using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do_an
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            User.loggedInUser = new User { MaNguoiDung = 0 };

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavPage();

            //khai bao cac navigation page
            new NavigationPage(new HomePage());
            new NavigationPage(new SellerPage());
            new NavigationPage(new ProductDetailPage());
            new NavigationPage(new CheckOutPage());
            new NavigationPage(new OrderSuccessPage());
            new NavigationPage(new UserProfilePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
