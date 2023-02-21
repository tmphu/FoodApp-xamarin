using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Do_an_WebAPI.Models;

namespace Do_an_WebAPI.Controllers
{
    public class Database
    {
        public static DataTable Read_Table(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            DataTable result = new DataTable("table1");
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(result);

                }
                catch (Exception ex)
                {

                }
            }
            return result;
        }
        public static object Exec_Command(string StoredProcedureName, Dictionary<string, object> dic_param = null)
        {
            string SQLconnectionString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
            object result = null;
            using (SqlConnection conn = new SqlConnection(SQLconnectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();

                // Start a local transaction.

                cmd.Connection = conn;
                cmd.CommandText = StoredProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dic_param != null)
                {
                    foreach (KeyValuePair<string, object> data in dic_param)
                    {
                        if (data.Value == null)
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@" + data.Key, data.Value);
                        }
                    }
                }
                cmd.Parameters.Add("@CurrentID", SqlDbType.Int).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@CurrentID"].Value;
                    // Attempt to commit the transaction.

                }
                catch (Exception ex)
                {

                    result = null;
                }

            }
            return result;
        }

        public static DataTable GetProductCategory()
        {
            return Read_Table("GetProductCategory");
        }

        public static DataTable GetSellerFeaturedList()
        {
            return Read_Table("GetSellerFeaturedList");
        }

        public static DataTable GetAllSeller()
        {
            return Read_Table("GetAllSeller");
        }
        public static DataTable GetSellerByCategory(int CategoryID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CategoryID", CategoryID);
            DataTable tb = Read_Table("GetSellerByCategory", param);
            return tb;
        }

        public static Seller GetSellerDetail(int sellerID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("SellerID", sellerID);
            DataTable tb = Read_Table("GetSellerDetail", param);
            Seller kq = new Seller();
            if (tb.Rows.Count > 0)
            {
                kq.SellerID = int.Parse(tb.Rows[0]["SellerID"].ToString());
                kq.SellerName = tb.Rows[0]["SellerName"].ToString();
                kq.SellerImage = tb.Rows[0]["SellerImage"].ToString();
                kq.SellerHeroBanner = tb.Rows[0]["SellerHeroBanner"].ToString();
                kq.SellerAddress = tb.Rows[0]["SellerAddress"].ToString();
                kq.SellerRating = float.Parse(tb.Rows[0]["SellerRating"].ToString());
                kq.isFeatured = bool.Parse(tb.Rows[0]["isFeatured"].ToString());
            }
            else
            {
                kq.SellerID = -1;
            }
            return kq;
        }

        public static DataTable GetProductBySeller(int sellerID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("sellerID", sellerID);
            DataTable tb = Read_Table("GetProductBySeller", param);
            return tb;
        }

        public static User AddUser(User u)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("TenNguoiDung", u.TenNguoiDung);
            param.Add("TenDangNhap", u.TenDangNhap);
            param.Add("MatKhau", u.MatKhau);
            param.Add("Email", u.Email);
            param.Add("SoDienThoai", u.SoDienThoai);
            param.Add("DiaChi", u.DiaChi);
            int kq = int.Parse(Exec_Command("AddUser", param).ToString());
            if (kq > -1)
            {
                u.MaNguoiDung = kq;
            }
            return u;
        }

        public static User updateUser(User u)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("MaNguoiDung", u.MaNguoiDung);
            param.Add("TenNguoiDung", u.TenNguoiDung);
            param.Add("TenDangNhap", u.TenDangNhap);
            param.Add("MatKhau", u.MatKhau);
            param.Add("Email", u.Email);
            param.Add("SoDienThoai", u.SoDienThoai);
            param.Add("DiaChi", u.DiaChi);
            int kq = int.Parse(Exec_Command("updateUser", param).ToString());
            if (kq > -1)
            {
                u.MaNguoiDung = kq;
            }
            return u;
        }

        public static User Login(string tenDangNhap, string matKhau)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("tendangnhap", tenDangNhap);
            param.Add("matkhau", matKhau);
            DataTable tb = Read_Table("getUser", param);
            User kq = new User();
            if (tb.Rows.Count > 0)
            {
                kq.MaNguoiDung = int.Parse(tb.Rows[0]["MaNguoiDung"].ToString());
                kq.TenNguoiDung = tb.Rows[0]["TenNguoiDung"].ToString();
                kq.TenDangNhap = tb.Rows[0]["TenDangNhap"].ToString();
                kq.MatKhau = tb.Rows[0]["MatKhau"].ToString();
                kq.Email = tb.Rows[0]["Email"].ToString();
                kq.SoDienThoai = tb.Rows[0]["SoDienThoai"].ToString();
                kq.DiaChi = tb.Rows[0]["DiaChi"].ToString();
            }
            else
            {
                kq.MaNguoiDung = -1;
            }
            return kq;
        }

        public static SalesOrder CreateSalesOrder(SalesOrder so)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CustomerID", so.CustomerID);
            param.Add("SellerID", so.SellerID);
            param.Add("OrderDate", so.OrderDate);
            param.Add("OrderStatus", so.OrderStatus);
            param.Add("ShippingAddress", so.ShippingAddress);
            param.Add("PaymentMethod", so.PaymentMethod);
            param.Add("TotalAmount", so.TotalAmount);

            int kq = int.Parse(Exec_Command("createSalesOrder", param).ToString());
            so.OrderID = kq;
            
            return so;
        }

        public static SalesOrderItem CreateSalesOrderItem(SalesOrderItem soi)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("OrderID", soi.OrderID);
            param.Add("ProductID", soi.ProductID);
            param.Add("ProductName", soi.ProductName);
            param.Add("Qty", soi.Qty);
            param.Add("ProductPrice", soi.ProductPrice);
            param.Add("TotalItemAmount", soi.TotalItemAmount);

            int kq = int.Parse(Exec_Command("createSalesOrderItem", param).ToString());
            soi.ItemID = kq;

            return soi;
        }

        public static DataTable GetOrderList(int CustomerID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("CustomerID", CustomerID);
            DataTable tb = Read_Table("GetOrderList", param);
            return tb;
        }

        public static DataTable GetOrderItem(int OrderID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("OrderID", OrderID);
            DataTable tb = Read_Table("GetOrderItem", param);
            return tb;
        }
    }
}