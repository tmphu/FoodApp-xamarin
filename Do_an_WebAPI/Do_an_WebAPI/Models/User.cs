using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_WebAPI.Models
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
    }
}