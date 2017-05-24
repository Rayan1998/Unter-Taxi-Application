﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
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
            await callAddRest();
            Console.WriteLine("Button clicked");
        }
        public static async Task callAddRest()
        {
            Request request = new Request(2, "from location", "to location");
            RestService restService = new RestService();
            await restService.SaveToItemAsync(request, true);
        }
    }
}