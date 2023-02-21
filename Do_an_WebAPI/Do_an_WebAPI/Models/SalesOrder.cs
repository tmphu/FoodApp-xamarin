using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_WebAPI.Models
{
    public class SalesOrder
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int SellerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalAmount { get; set; }
        public string TotalAmountVND { get { return this.TotalAmount + " đ"; } }
    }
}