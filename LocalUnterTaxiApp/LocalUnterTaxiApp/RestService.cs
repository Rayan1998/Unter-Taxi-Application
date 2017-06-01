using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Newtonsoft.Json.Linq;

//Author: Rayan El Hajj 

namespace LocalUnterTaxiApp
{
    /**
     * RestService creates an HTTP client with corresponding methods to be used throughout the application 
     * this class is a client of the RESTful web service for the system written on php and accessed on a remote Apache webserver 
     */ 
    class RestService 
    {

        HttpClient client;
        
        public RestService()
        {
            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 100000; //the MaxResponseContentBufferSize property is used to specify the max nb of bytes to buffer when reading the content in the HTTP response message
        }

        /**
         * posts a request to the db using a RESTful web service 
         * prints out to the console in case of success 
         * this method has to be asynchronous and to return Task that is a sort of threadpool 
         */
        public async Task PostRequestAsync(Request request)
        {
            string RestUrl = "http://360itsolutions.dk/RESTApi.php/request/";
            var uri = new Uri(RestUrl);
            //creating JValue objects with the request fields 
            //the JValue class represents a value in JSON
            JValue customer_ID = new JValue(request.Customer_ID); 
            JValue from_Location = new JValue(request.From_Location);
            JValue to_Location = new JValue(request.To_Location);
            //JObject represents a JSON Object 
            JObject request_json = new JObject();
            //adding the JValue objects to the JObject with the reference request_json
            request_json.Add("FK_Customer_ID", customer_ID);
            request_json.Add("From_Location", from_Location);
            request_json.Add("To_Location", to_Location);
            //setting the json string as the string representation of the request_json JObject
            string json = request_json.ToString();
            Console.WriteLine(json);
            //the StringContent sets the string given as argument as the content of the HTTP request in the body part of it 
            var content = new StringContent(json, Encoding.UTF8, "application/json"); //application/json is to specify the media type / Content type 
            //PostAsync sends a post HTTP request to the server ( i.e. the RESTful web service written in php) 
            HttpResponseMessage response = await client.PostAsync(uri,content);

            if (response.IsSuccessStatusCode) //the IsSuccessStatusCode property is to indicate whether the HTTP request succeeded or failed
            {
                Console.WriteLine("Request successfully saved.");//print out to the console for debugging purposes 
            }

        }

        /**
         * posts a customer to the db using a RESTful web service 
         * prints out to the console in case of success
         * this method has to be asynchronous and to return Task that is a sort of threadpool 
         */ 
        public async Task PostCustomerAsync(Customer customer)
        {
            string RestUrl = "http://360itsolutions.dk/RESTApi.php/_customer/";
            var uri = new Uri(RestUrl);
            //Console.WriteLine("Before Javalues!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); //for debugging purpose

            //creating JValue objects with the customer fields 
            JValue f_name = new JValue(customer.FName);
            JValue l_name = new JValue(customer.LName);
            JValue email = new JValue(customer.Email);
            JValue username = new JValue(customer.Username);
            JValue password = new JValue(customer.Password);
            JValue phone_nb = new JValue(customer.PhoneNB);
            JValue preferred_Brand = new JValue(customer.Preffered_Brand);

            //Console.WriteLine("After Javalues, before JObjects!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes

            //JObject represents a JSON object 
            JObject credentials_json = new JObject();
            JObject customer_json = new JObject();

            //Console.WriteLine("After JObjecs!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); //for debugging purposes

            //adding the JValue objects to the JObject with the reference customer_json
            customer_json.Add("FName", f_name);
            customer_json.Add("LName", l_name);
            customer_json.Add("PhoneNb", phone_nb);
            customer_json.Add("Preferred_Brand", preferred_Brand);

            //Console.WriteLine("After custoer_json.Add(stuff)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes

            //adding the JValue objects to the JObject with the reference credentials_json
            credentials_json.Add("Email", email);
            credentials_json.Add("Username",username);
            credentials_json.Add("Password",password);


            //Console.WriteLine("After credentials_json.Add(stuff)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes
            //Console.WriteLine(credentials_json.ToString());//for debugging purposes
            //Console.WriteLine(customer_json.ToString());//for debugging purposes

            //adding the two objects to a third object

            JObject objects_object = new JObject();
            objects_object.Add("Credentials", credentials_json);
            objects_object.Add("Customer", customer_json);
            //Console.WriteLine("After credentials_json.Add(stuff) and console writeing!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes
            
            //getting the json string representation of the objects_object JObject 
            string json = objects_object.ToString(); 
            //Console.WriteLine(json);//for debugging purposes
            //the StringContent sets the string given as argument as the content of the HTTP request in the body part of it 
            var content = new StringContent(json, Encoding.UTF8, "application/json"); //application/json is to specify the media type / Content type 

            //Console.WriteLine("After content, before response!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes
            
            //PostAsync sends a post HTTP request to the server ( i.e. the RESTful web service written in php) 
            HttpResponseMessage response = await client.PostAsync(uri, content);

            //Console.WriteLine("After response!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");//for debugging purposes 

            if (response.IsSuccessStatusCode) //the IsSuccessStatusCode property is to indicate whether the HTTP request succeeded or failed
             {
                 Console.WriteLine("Request successfully saved.");//print out to the console for debugging purposes 
             }
        }
    }
}
