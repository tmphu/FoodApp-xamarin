using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_WebAPI.Models
{
    public class SalesOrderItem
    {
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int ProductPrice { get; set; }
        public int TotalItemAmount { get { return this.Qty * this.ProductPrice; } }
        public string TotalItemAmountVND { get { return this.TotalItemAmount + " đ"; } }
    }
}