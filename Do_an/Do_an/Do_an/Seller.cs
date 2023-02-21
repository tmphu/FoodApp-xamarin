using System;
using System.Collections.Generic;
using System.Text;

namespace Do_an
{
    public class Seller
    {
        public int SellerID { get; set; }
        public string SellerName { get; set; }
        public string SellerImage { get; set; }
        public string SellerHeroBanner { get; set; }
        public string SellerAddress { get; set; }
        public float SellerRating { get; set; }
        public bool isFeatured { get; set; }
        public string NameAddress { get { return this.SellerName + " - " + this.SellerAddress; } }
    }
}
