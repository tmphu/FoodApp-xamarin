using System;
using System.Collections.Generic;
using System.Text;

namespace Do_an
{
    public class User
    {
        public int MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public static User loggedInUser;
    }
}
