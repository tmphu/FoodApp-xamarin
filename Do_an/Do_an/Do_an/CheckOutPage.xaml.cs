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
    public partial class CheckOutPage : ContentPage
    {
        public Product selectedProduct;
        public Seller selectedSeller;
        public SalesOrderItem salesOrderItem;
        public User u;
        public CheckOutPage()
        {
            InitializeComponent();
        }
        public CheckOutPage(Product p, int qty, Seller sl)
        {
            InitializeComponent();
            this.selectedProduct = p;
            this.selectedSeller = sl;
            salesOrderItem = CreateQuotation(selectedProduct, qty);
            orderDetail.BindingContext = salesOrderItem;

            u = User.loggedInUser;
            userName.BindingContext = u;
            shippingAddress.BindingContext = u;
            this.Content.BindingContext = salesOrderItem;
        }
        public SalesOrderItem CreateQuotation(Product p, int qty)
        {
            SalesOrderItem soi = new SalesOrderItem();
            soi.ProductID = p.ProductID;
            soi.ProductName = p.ProductName;
            soi.Qty = qty;
            soi.ProductPrice = p.ProductPrice;
            return soi;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //create sales order
            SalesOrder so = new SalesOrder
            {
                CustomerID = u.MaNguoiDung,
                SellerID = selectedSeller.SellerID,
                OrderDate = DateTime.Now,
                OrderStatus = "Đang chuẩn bị",
                ShippingAddress = shippingAddress.Text,
                PaymentMethod = paymentMethod.Content.ToString(),
                TotalAmount = salesOrderItem.TotalItemAmount
            };
            string jsonSalesOrder = JsonConvert.SerializeObject(so);
            StringContent httpContent = new StringContent(jsonSalesOrder, Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();
            HttpResponseMessage kq = await http.PostAsync
                                ("http://10.0.0.112/DoAnWebAPI/api/XulyController/CreateSalesOrder", httpContent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            so = JsonConvert.DeserializeObject<SalesOrder>(kqtv);

            if (so.OrderID > 0)
            {
                //create sales order item
                SalesOrderItem soi = new SalesOrderItem
                {
                    OrderID = so.OrderID,
                    ProductID = salesOrderItem.ProductID,
                    ProductName = salesOrderItem.ProductName,
                    Qty = salesOrderItem.Qty,
                    ProductPrice = salesOrderItem.ProductPrice
                };
                string jsonSalesOrderItem = JsonConvert.SerializeObject(soi);
                StringContent httpContentSOI = new StringContent(jsonSalesOrderItem, Encoding.UTF8, "application/json");

                HttpClient httpSOI = new HttpClient();
                HttpResponseMessage kqSOI = await httpSOI.PostAsync
                                    ("http://10.0.0.112/DoAnWebAPI/api/XulyController/CreateSalesOrderItem", httpContentSOI);
                var kqtvSOI = await kqSOI.Content.ReadAsStringAsync();
                soi = JsonConvert.DeserializeObject<SalesOrderItem>(kqtvSOI);

                await Navigation.PushAsync(new OrderSuccessPage(so, soi));
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra!", "OK");
            }
        }
    }
}