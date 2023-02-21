using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_WebAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SellerID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}