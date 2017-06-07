using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage ()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromRgb(255, 204, 0);
            BarTextColor = Color.Black;
        }
    }
}