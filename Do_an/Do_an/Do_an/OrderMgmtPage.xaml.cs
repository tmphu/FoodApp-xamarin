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
    public partial class OrderMgmtPage : ContentPage
    {
        public User u;
        public OrderMgmtPage()
        {
            InitializeComponent();
            u = User.loggedInUser;
            ShowOrderList();
        }
        public async void ShowOrderList()
        {
            try
            {
                HttpClient http = new HttpClient();
                var kq = await http.GetStringAsync
                         ("http://10.0.0.112/DoAnWebAPI/api/XulyController/GetOrderList?CustomerID=" + u.MaNguoiDung);
                var orders = JsonConvert.DeserializeObject<List<SalesOrder>>(kq);
                orderList.ItemsSource = orders;
            }
            catch
            {
                StackLayout st = new StackLayout();
                Label l1 = new Label();
                l1.Text = "Bạn chưa có đơn hàng nào!";
                l1.HorizontalOptions = LayoutOptions.Center;
                l1.FontSize = 24;
                st.Children.Add(l1);
                this.Content = st;
            }
        }

        private void orderList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SalesOrder salesOrder = (SalesOrder)e.SelectedItem;
            Navigation.PushAsync(new OrderDetailPage(salesOrder));
        }
    }
}