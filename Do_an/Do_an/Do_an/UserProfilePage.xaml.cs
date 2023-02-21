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
    public partial class UserProfilePage : ContentPage
    {
        public User u;
        public UserProfilePage()
        {
            InitializeComponent();
            u = User.loggedInUser;
            ShowUserInfo();
            this.Content.BindingContext = u;
        }

        void ShowUserInfo()
        {
            if (u.MaNguoiDung > 0)
            {
                txtUserName.Text = u.TenNguoiDung;
                txtLoginName.Text = u.TenDangNhap;
                txtPassword.Text = u.MatKhau;
                txtPasswordConfirm.Text = u.MatKhau;
                txtEmail.Text = u.Email;
                txtPhone.Text = u.SoDienThoai;
                txtAddress.Text = u.DiaChi;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool kq = await DisplayAlert("Thông báo", "Bạn có chắn chắn muốn đăng xuất?", "Có, đăng xuất", "Không");
            if (kq)
            {
                User.loggedInUser = null;
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                return;
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng!", "OK");
                return;
            };

            User user = new User
            {
                MaNguoiDung = u.MaNguoiDung,
                TenNguoiDung = txtUserName.Text,
                TenDangNhap = txtLoginName.Text,
                MatKhau = txtPassword.Text,
                Email = txtEmail.Text,
                SoDienThoai = txtPhone.Text,
                DiaChi = txtAddress.Text
            };
            string jsonUser = JsonConvert.SerializeObject(user);
            StringContent httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();
            HttpResponseMessage kq = await http.PostAsync
                                ("http://10.0.0.112/DoAnWebAPI/api/XulyController/updateUser", httpContent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<User>(kqtv);

            if (user.MaNguoiDung > 0)
            {
                await DisplayAlert("Thông báo", "Cập nhật thông tin thành công!", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
            }
        }
    }
}