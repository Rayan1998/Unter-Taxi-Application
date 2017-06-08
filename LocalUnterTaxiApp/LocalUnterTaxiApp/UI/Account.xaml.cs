using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Account : ContentPage
	{
		public Account ()
		{
			InitializeComponent ();
		}



        private void Save_Btn_Clicked(object sender, EventArgs e)
        {

        }

        private void logout_Btn_Clicked(object sender, EventArgs e)
        {
            Session.Current_Customer = null;
            Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromRgb(255, 204, 0),//this color is yellow but not too bright 
                BarTextColor = Color.Black,
            };
        }
    }
}