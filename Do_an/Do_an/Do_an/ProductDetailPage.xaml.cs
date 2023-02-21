using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Do_an
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public Product selectedProduct;
        public Seller selectedSeller;
        public ProductDetailPage()
        {
            InitializeComponent();
        }
        public ProductDetailPage(Product p, Seller sl)
        {
            InitializeComponent();
            this.selectedProduct = p;
            this.selectedSeller = sl;
            productHeader.BindingContext = selectedProduct;
            //this.Title = selectedSeller.NameAddress;
            title.BindingContext = selectedSeller;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int qty = int.Parse(stepper.Value.ToString());
            Navigation.PushAsync(new CheckOutPage(selectedProduct, qty, selectedSeller));
        }
    }
}