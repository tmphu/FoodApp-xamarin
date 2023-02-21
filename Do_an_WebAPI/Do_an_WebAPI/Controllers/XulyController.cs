using Do_an_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Do_an_WebAPI.Controllers
{
    public class XulyController : ApiController
    {
        [Route("api/XulyController/AddUser")]
        [HttpPost]
        public IHttpActionResult AddUser(User u)
        {
            try
            {
                User kq = Database.AddUser(u);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/updateUser")]
        [HttpPost]
        public IHttpActionResult updateUser(User u)
        {
            try
            {
                User kq = Database.updateUser(u);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/Login")]
        [HttpGet]
        public IHttpActionResult Login(string tenDangNhap, string matKhau)
        {
            try
            {
                User u = Database.Login(tenDangNhap, matKhau);
                return Ok(u);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetSellerDetail")]
        [HttpGet]
        public IHttpActionResult GetSellerDetail(int sellerID)
        {
            try
            {
                Seller sl = Database.GetSellerDetail(sellerID);
                return Ok(sl);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetProductCategory")]
        [HttpGet]
        public IHttpActionResult GetProductCategory()
        {
            DataTable tb;
            tb = Database.GetProductCategory();
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetSellerFeaturedList")]
        [HttpGet]
        public IHttpActionResult GetSellerFeaturedList()
        {
            DataTable tb;
            tb = Database.GetSellerFeaturedList();
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetAllSeller")]
        [HttpGet]
        public IHttpActionResult GetAllSeller()
        {
            DataTable tb;
            tb = Database.GetAllSeller();
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetSellerByCategory")]
        [HttpGet]
        public IHttpActionResult GetSellerByCategory(int CategoryID)
        {
            DataTable tb;
            tb = Database.GetSellerByCategory(CategoryID);
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetProductBySeller")]
        [HttpGet]
        public IHttpActionResult GetProductBySeller(int sellerID)
        {
            DataTable tb;
            tb = Database.GetProductBySeller(sellerID);
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/CreateSalesOrder")]
        [HttpPost]
        public IHttpActionResult CreateSalesOrder(SalesOrder so)
        {
            try
            {
                SalesOrder kq = Database.CreateSalesOrder(so);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/CreateSalesOrderItem")]
        [HttpPost]
        public IHttpActionResult CreateSalesOrderItem(SalesOrderItem soi)
        {
            try
            {
                SalesOrderItem kq = Database.CreateSalesOrderItem(soi);
                return Ok(kq);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetOrderList")]
        [HttpGet]
        public IHttpActionResult GetOrderList(int CustomerID)
        {
            DataTable tb;
            tb = Database.GetOrderList(CustomerID);
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/XulyController/GetOrderItem")]
        [HttpGet]
        public IHttpActionResult GetOrderItem(int OrderID)
        {
            DataTable tb;
            tb = Database.GetOrderItem(OrderID);
            if (tb != null && tb.Rows.Count > 0)
            {
                return Ok(tb);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
