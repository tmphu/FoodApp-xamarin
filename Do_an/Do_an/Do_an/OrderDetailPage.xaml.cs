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
    public partial class OrderDetailPage : ContentPage
    {
        public SalesOrder selectedSalesOrder;
        public SalesOrderItem selectedSalesOrderItem;
        public User u;
        public OrderDetailPage()
        {
            InitializeComponent();
        }
        public OrderDetailPage(SalesOrder so)
        {
            InitializeComponent();
            this.selectedSalesOrder = so;
            u = User.loggedInUser;
            user.BindingContext = u;
            user2.BindingContext = u;
            user3.BindingContext = u;
            ShowOrder();
            ShowOrderItem();
        }

        public void ShowOrder()
        {
            Order.BindingContext = selectedSalesOrder;
        }

        public async void ShowOrderItem()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetOrderItem?OrderID=" + selectedSalesOrder.OrderID);
            var salesOrderItems = JsonConvert.DeserializeObject<List<SalesOrderItem>>(kq);
            orderDetail.ItemsSource = salesOrderItems;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                     ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetSellerDetail?sellerID=" + selectedSalesOrder.SellerID);
            var sellers = JsonConvert.DeserializeObject<Seller>(kq);

            await Navigation.PushAsync(new SellerPage(sellers));
        }
    }
}