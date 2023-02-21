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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void cmdSignUp_Clicked(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng!", "OK");
                return;
            };

            User u = new User { TenNguoiDung = txtUserName.Text,
                                TenDangNhap = txtLoginName.Text,
                                MatKhau = txtPassword.Text,
                                Email = txtEmail.Text,
                                SoDienThoai = txtPhone.Text };
            string jsonUser = JsonConvert.SerializeObject(u);
            StringContent httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            
            HttpClient http = new HttpClient();
            HttpResponseMessage kq = await http.PostAsync
                                ("http://10.0.0.112/DoAnWebAPI/api/XulyController/AddUser", httpContent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            u = JsonConvert.DeserializeObject<User>(kqtv);

            if (u.MaNguoiDung > 0)
            {
                await DisplayAlert("Thông báo", "Thêm người dùng thành công: " + u.TenDangNhap, "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Thông báo", "Tên đăng nhập đã tồn tại: " + u.TenDangNhap, "OK");
            }
        }
    }
}