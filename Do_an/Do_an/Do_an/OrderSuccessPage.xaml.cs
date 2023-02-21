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
    public partial class OrderSuccessPage : ContentPage
    {
        public SalesOrder so;
        public SalesOrderItem soi;
        
        public OrderSuccessPage()
        {
            InitializeComponent();
        }
        public OrderSuccessPage(SalesOrder so, SalesOrderItem soi)
        {
            InitializeComponent();
            this.so = so;
            this.soi = soi;
            header.BindingContext = so;
        }

        private void toHomePage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavPage());
        }
    }
}