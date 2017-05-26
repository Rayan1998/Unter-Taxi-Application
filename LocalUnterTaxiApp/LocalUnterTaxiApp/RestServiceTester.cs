using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;

namespace LocalUnterTaxiApp
{
    public class RestServiceTester
    {

        public static async void Main(string[] args)
        {
            await callAddRest();

        }
        public static async Task callAddRest()
        {
            Request request = new Request(2, "from location", "to location");
            RestService restService = new RestService();
            await restService.SaveToItemAsync(request);
        }
    }
}
