using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_an_WebAPI.Models
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