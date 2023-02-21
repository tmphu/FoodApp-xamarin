using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do_an
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerPage : ContentPage
    {
        public Seller selectedSeller;
        public SellerPage()
        {
            InitializeComponent();
        }
        public SellerPage(Seller sl)
        {
            InitializeComponent();
            this.selectedSeller = sl;
            ShowSellerHeader();
            ShowSellerProduct();
        }
        public async void ShowSellerHeader()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetSellerDetail?sellerID=" + selectedSeller.SellerID);
            var sellers = JsonConvert.DeserializeObject<Seller>(kq);
            sellerHeader.BindingContext = sellers;
        }

        public async void ShowSellerProduct()
        {
            try
            {
                HttpClient http = new HttpClient();
                var kq = await http.GetStringAsync
                         ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetProductBySeller?sellerID=" + selectedSeller.SellerID);
                var products = JsonConvert.DeserializeObject<List<Product>>(kq);
                sellerProduct.ItemsSource = products;
            }
            catch
            {
                
            }
        }

        private void sellerProduct_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product selected_product;
            if (e.SelectedItemIndex >= 0)
            {
                selected_product = (Product)e.SelectedItem;
                Navigation.PushAsync(new ProductDetailPage(selected_product, selectedSeller));
                sellerProduct.SelectedItem = null;
            }
        }
    }
}