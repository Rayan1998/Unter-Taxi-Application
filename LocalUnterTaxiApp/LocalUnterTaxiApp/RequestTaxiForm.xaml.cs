using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//Author: Rayan El Hajj
namespace LocalUnterTaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestTaxiForm : ContentPage
	{
		public RequestTaxiForm ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CallAddRest(); 
            Console.WriteLine("Button clicked");//only for debugging purposes 
        }

        /**
         * creates instance of RestService class & invokes proper posting method 
         * this method has to be at least asynch
         */
        public static async Task CallAddRest()
        {
            Request request = new Request(2, "hardcoded from location", "to location");//to be removed in future enhancment, shld be replaced with a form filled by user
            //create instance of the HTTP  client class
            RestService restService = new RestService();
            //calling the post method on the created instance of the class 
            await restService.PostRequestAsync(request);
        }
    }
}