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
            Console.WriteLine("Button clicked");
            /* Request request = new Request(29, "from location", "to location");
             JArray array = new JArray();
             JValue customer_ID = new JValue(request.Customer_ID);
             JValue from_Location = new JValue(request.From_Location);
             JValue to_Location = new JValue(request.To_Location);
             array.Add( customer_ID );
             array.Add( from_Location);
             array.Add(to_Location);
             Console.WriteLine( array.ToString());*/
        }
        public static async Task CallAddRest()
        {
            Request request = new Request(2, "from location", "to location");
            RestService restService = new RestService();
            await restService.SaveToItemAsync(request);
        }
    }
}