using Newtonsoft.Json;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do_an
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            InitHomePage();
        }
        void InitHomePage()
        {
            var u = User.loggedInUser;

            ShowBanner();
            showCategory();
            ShowSellerFeaturedList();
            ShowAllSeller();
        }
        void ShowBanner()
        {
            List<Banner> banners = new List<Banner>();
            banners.Add(new Banner { Id = 1, Image = "banner_1.jpeg" });
            banners.Add(new Banner { Id = 2, Image = "banner_2.jpeg" });
            banners.Add(new Banner { Id = 3, Image = "banner_3.jpg" });
            banners.Add(new Banner { Id = 4, Image = "banner_4.jpeg" });
            bannerCarousel.ItemsSource = banners;

            // function auto scroll banner
            Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            {
                bannerCarousel.Position = (bannerCarousel.Position + 1) % banners.Count;
                return true;
            }));
        }
        public async void showCategory()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetProductCategory");
            var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(kq);
            iconCategory.ItemsSource = categories;
        }

        public async void ShowSellerFeaturedList()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetSellerFeaturedList");
            var sellers = JsonConvert.DeserializeObject<List<Seller>>(kq);
            sellerFeaturedList.ItemsSource = sellers;
        }

        public async void ShowAllSeller()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetAllSeller");
            var sellers = JsonConvert.DeserializeObject<List<Seller>>(kq);
            sellerAll.ItemsSource = sellers;
        }

        private void sellerFeaturedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Seller selected_seller;
            if (e.CurrentSelection != null)
            {
                selected_seller = (Seller)e.CurrentSelection.FirstOrDefault();
                Navigation.PushAsync(new SellerPage(selected_seller));
            }
        }
        private void iconCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductCategory selected_category;
            if (e.CurrentSelection != null)
            {
                selected_category = (ProductCategory)e.CurrentSelection.FirstOrDefault();
                Navigation.PushAsync(new CategoryPage(selected_category));
            }
        }

        private void sellerAll_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Seller selected_seller;
            if (e.SelectedItemIndex >= 0)
            {
                selected_seller = (Seller)e.SelectedItem;
                Navigation.PushAsync(new SellerPage(selected_seller));
                sellerAll.SelectedItem = null;
            }
        }

       
    }
}