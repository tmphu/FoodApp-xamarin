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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void cmdLogin_Clicked(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtPassword.Text) )
            {
                await DisplayAlert("Thông báo", "Tên đăng nhập và mật khẩu không được để trống!", "OK");
            }
            else
            {
                HttpClient http = new HttpClient();
                var kq = await http.GetStringAsync
                        ("http://10.0.0.112/DoAnWebAPI/api/XulyController/Login?tenDangNhap=" + txtLoginName.Text + "&&matKhau=" + txtPassword.Text);
                var u = JsonConvert.DeserializeObject<User>(kq);
                if (u.MaNguoiDung > 0)
                {
                    User.loggedInUser = u;
                    await Navigation.PushAsync(new NavPage());
                }
                else
                {
                    await DisplayAlert("Thông báo", "Đăng nhập thất bại. Tên đăng nhập hoặc mật khẩu sai.", "OK");
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}