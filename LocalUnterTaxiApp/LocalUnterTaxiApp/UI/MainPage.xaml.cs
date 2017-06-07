using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalUnterTaxiApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

        }

        private void login_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private void register_btn_Clicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new Register());
            Navigation.PushAsync(new Register());
        }
    }
}
