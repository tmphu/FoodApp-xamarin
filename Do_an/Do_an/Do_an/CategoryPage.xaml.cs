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
    public partial class CategoryPage : ContentPage
    {
        public ProductCategory selectedCategory;
        public CategoryPage()
        {
            InitializeComponent();
        }
        public CategoryPage(ProductCategory cate)
        {
            InitializeComponent();
            this.selectedCategory = cate;
            showSellerByCategory();
        }
        async void showSellerByCategory()
        {
            try
            {
                HttpClient http = new HttpClient();
                var kq = await http.GetStringAsync
                         ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetSellerByCategory?CategoryID=" + selectedCategory.CategoryID);
                var sellers = JsonConvert.DeserializeObject<List<Seller>>(kq);
                sellerList.ItemsSource = sellers;
            }
            catch
            {

            }
        }

        private void sellerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Seller selected_seller;
            if (e.SelectedItemIndex >= 0)
            {
                selected_seller = (Seller)e.SelectedItem;
                Navigation.PushAsync(new SellerPage(selected_seller));
                sellerList.SelectedItem = null;
            }
        }
    }
}