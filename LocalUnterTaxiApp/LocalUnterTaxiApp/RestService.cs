using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

//Author: Rayan El Hajj 
//new IP Add: 87.54.141.140

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
            string RestUrl = "http://87.54.141.140/WebService/RESTApi.php/request"; 
            var uri = new System.Uri(RestUrl);
            //creating JValue objects with the request fields 
            //the JValue class represents a value in JSON
            JValue credentials_ID = new JValue(request.Credentials_ID);
            JValue from_Location = new JValue(request.From_Location);
            JValue to_Location = new JValue(request.To_Location);
            //JObject represents a JSON Object 
            JObject request_json = new JObject();
            //adding the JValue objects to the JObject with the reference request_json
            request_json.Add("FK_credentials_ID", credentials_ID);
            request_json.Add("From_Location", from_Location);
            request_json.Add("To_Location", to_Location);
            //setting the json string as the string representation of the request_json JObject
            string json = request_json.ToString();
            Console.WriteLine(json);
            //the StringContent sets the string given as argument as the content of the HTTP request in the body part of it 
            var content = new StringContent(json, Encoding.UTF8, "application/json"); //application/json is to specify the media type / Content type 
            //PostAsync sends a post HTTP request to the server ( i.e. the RESTful web service written in php) 
            HttpResponseMessage response = await client.PostAsync(uri, content);


            if (await response.Content.ReadAsStringAsync() == "true")
            {
                SynchronousSQLite.Initialize();
                SynchronousSQLite.addRequest(SynchronousSQLite.Connection, request.From_Location, request.To_Location);
                await Application.Current.MainPage.DisplayAlert("Success", "Dear" + Session.Current_Customer.Username + ",\n We sent a request to the dispatcher with the information,\n Confirmation on order will be sent to the email address: " + Session.Current_Customer.Email, "OK");
            }
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
            string RestUrl = "http://87.54.141.140/WebService/RESTApi.php/_customer";
            var uri = new System.Uri(RestUrl);
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
            credentials_json.Add("Username", username);
            credentials_json.Add("Password", password);


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

            if (await response.Content.ReadAsStringAsync() == "true")
            {
                string from_Add = "unterdevelopmentteam@gmail.com";
                sendConfirmationEmail(from_Add,customer.Email,"Confirmation for Registration","Dear "+customer.Username+"we would like to confirm your registration in our system");
            }

            /*if (response.IsSuccessStatusCode) //the IsSuccessStatusCode property is to indicate whether the HTTP request succeeded or failed
             {
                 Console.WriteLine("Request successfully saved.");//print out to the console for debugging purposes 
             }*/
        }

        //needed for the  functions to send  confirmation email to customers 
        bool is_Sent = false;

        /**
         * creates MailMessage object, adds the fields for the mail message
         * creates an SmtpClient object to gmail smtp server 
         * sends the emails to customers from a special gmail for the unter development team 
         */
        private void sendConfirmationEmail(string from, string to, string subject, string msg)
        {
            //Console.WriteLine("Start of method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); //for debugging purposes 
            MailMessage mail_msg = new MailMessage();
            mail_msg.From = new MailAddress(from);
            mail_msg.To.Add(new MailAddress(to));
            mail_msg.Subject = subject;
            mail_msg.Body = msg;

            try
            {
               var smtp_client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("unterdevelopmentteam@gmail.com", "UnterApplication1234"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtp_client.SendCompleted += new SendCompletedEventHandler(sendCompletedCallback);
                string userState = "test message from acp ";
                smtp_client.SendAsync(mail_msg, userState);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                mail_msg = null;
            }
        }

        /**
         * gets the token for the event of send email completion 
         * prints out to the console any cancellation, errors, or success 
         * set the boolean is_Sent to true or false based on the result of the transaction 
         */ 
        private void sendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //Console.WriteLine("Start of handlerrrrr method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); //for debugging purposes 
            String token = (String)e.UserState;
            //printing out if the event was cancelled
            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send was Cancelled",token);
            }
            //printing out to the console if the event was completed with errors 
            if(e.Error!= null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            //printing out to the console if the event is successfully completed 
            else
            {
                Console.WriteLine("Message sent.");
            }
            is_Sent = true;
        }

        public async Task ValidateCredentials(string username, string password)
        {
            //Console.WriteLine("Starttt of methodddd!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"); // for debugging purposes
            //Console.WriteLine(username + password);// for debugging purposes
            string URL = "http://87.54.141.140/WebService/RESTApi.php/credentials/validation";
            var uri = new System.Uri(URL);
            //creating JValue objects with the credentials 
            //the JValue class represents a value in JSON
            JValue userName = new JValue(username);
            JValue passWord = new JValue(password);
            //JObject represents a JSON Object 
            JObject cred_Object = new JObject();
            //adding the JValue objects to the JObject with the reference cred_Object
            cred_Object.Add("Username", userName);
            cred_Object.Add("Password", passWord);
            //the StringContent sets the string given as argument as the content of the HTTP request in the body part of it 
            string cred_json = cred_Object.ToString();
            Console.WriteLine(cred_json);
            var content = new StringContent(cred_json, Encoding.UTF8,"application/json" );
            //PostAsync sends a post HTTP request to the server ( i.e. the RESTful web service written in php) 
            HttpResponseMessage response = await client.PostAsync(uri, content);
            string response_msg= await response.Content.ReadAsStringAsync();
            Console.WriteLine(response_msg);
            JArray response_array= JArray.Parse(response_msg);
            Console.WriteLine(response_array.ToString());
            if (response_array.HasValues== false) {
                await Application.Current.MainPage.DisplayAlert("Login failed!", "Invalid username or password", "OK");
                //Console.WriteLine("in the if statement!!!!!!!!!!!!!!");// for debugging purposes
            }
            else {
                foreach (JObject item in response_array)
                {
                    int ID =(int)(item.GetValue("ID"));
                    //Console.WriteLine(ID + "!!!!!!!!");// for debugging purposes
                    string _username = item.GetValue("Username").ToString();
                    Console.WriteLine(_username);
                    string _password = item.GetValue("Password").ToString();
                    Console.WriteLine(_password);
                    string email = item.GetValue("Email").ToString();
                    Console.WriteLine(email);
                    Session.Current_Customer = new Customer(ID,email,_username,_password);
                }
                
            }
        }

        public async Task DeactivateAccount(int credentials_ID)
        {
            string URL = "http://87.54.141.140/WebService/RESTApi.php/credentials/"+credentials_ID;
            var uri = new System.Uri(URL);
            /*JValue cred_ID = new JValue(credentials_ID);
            JObject json_Obj = new JObject();
            json_Obj.Add("ID", cred_ID);
            string json = json_Obj.ToString();
            var content = new StringContent(json,Encoding.UTF8,"application/json");*/
            HttpResponseMessage response = await client.DeleteAsync(uri);
            string response_msg = await response.Content.ReadAsStringAsync();
            if (response_msg.Contains("true"))
            {
                Application.Current.MainPage= new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = Color.FromRgb(255, 204, 0),//this color is yellow but not too bright 
                    BarTextColor = Color.Black,
                };

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed!", "Failed to deactivate the Account,\n Please Try again later!", "OK");
            }
        }
    }
}
