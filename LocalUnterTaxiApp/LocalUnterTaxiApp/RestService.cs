using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Newtonsoft.Json.Linq;

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


        public async Task SaveToItemAsync(Request request)
        {
            string RestUrl = "http://360itsolutions.dk/RESTApi.php/Request/";
            var uri = new Uri(RestUrl);

            JValue customer_ID = new JValue(request.Customer_ID);
            JValue from_Location = new JValue(request.From_Location);
            JValue to_Location = new JValue(request.To_Location);
            
            JObject request_json = new JObject();

            request_json.Add("FK_Customer_ID", customer_ID);
            request_json.Add("From_Location", from_Location);
            request_json.Add("To_Location", to_Location);
            
            string json = request_json.ToString();
            Console.WriteLine(json);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri,content);
           

            if (response.IsSuccessStatusCode) //the IsSuccessStatusCode property is to indicate whether the HTTP request succeeded or failed
            {
                Console.WriteLine("Request successfully saved.");
            }

        }
    }
}
