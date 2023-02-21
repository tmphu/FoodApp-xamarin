using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do_an
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SellerID { get; set; }
        public int ProductPrice { get; set; }
        public string ProductPriceVND { get { return this.ProductPrice + " đ"; } }
        public string ProductImage { get; set; }
    }
}
