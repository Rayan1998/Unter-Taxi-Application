using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Newtonsoft.Json;

namespace LocalUnterTaxiApp
{
    class RestService 
    {

        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000; //the MaxResponseContentBufferSize property is used to specify the max nb of bytes to buffer when reading the content in the HTTP response message
        }


        public async Task SaveToItemAsync(Request request, bool isNewItem=false)
        {
            string RestUrl = "http://10.176.165.46/RESTapi/Request/"+ request.From_Location+","+request.To_Location+","+request.Customer_ID;
            var uri = new Uri(RestUrl);

            var json = JsonConvert.SerializeObject(request);//SerializeObject method returns a json representation of the given object 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            if (isNewItem)
            {
                response = await client.PostAsync(uri,content);
            }

            if (response.IsSuccessStatusCode) //the IsSuccessStatusCode property is to indicate whether the HTTP request succeeded or failed
            {
                Console.WriteLine(@"Request successfully saved.");
            }

        }
    }
}
