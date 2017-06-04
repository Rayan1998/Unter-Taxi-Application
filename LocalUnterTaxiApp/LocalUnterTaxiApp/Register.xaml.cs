using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        bool areValid = true;

        public Register()
        {
            InitializeComponent();
        }

        /**
         * checks for entries to validate their formats and creates a customer Object with the information 
         * creates instance of RestService and calls the method to post the customer through the RestApi
         */
        private async void Register_Btn_Clicked(object sender, EventArgs e)
        {
            //checking entries 
            checkEntries();
            //if all the entries are valid, create customer and RestService instance and call PostCustomerAsync method 
            if (areValid == true)
            {
                await CallAddRest();
            }

        }

        /**
         * creates the customer object 
         * creates instance of RestService class
         * invokes post method for the customer PostCustomerAsync()
         * this method should be async and return a Task which is a sort of Thread pool 
         */
        public async Task CallAddRest()
        {
            Customer customer = new Customer(fName.Text, lName.Text, e_Mail.Text, userName.Text, passWord.Text, phoneNb.Text, BrandPicker.SelectedItem.ToString());//"OH", "Right", "wewerehardcoding@stuff", "letmetakeaselfie", "yeahlethertakeaselfie", "1234567890", "Rise"); //(
            //Console.WriteLine(customer.FName);
            RestService rest = new RestService();
            await rest.PostCustomerAsync(customer);
        }

        /**
         * checks if user entries are in an appropriate format using Regular Expressions 
         * sets the areValid boolean to true or false depending on the result of the check 
         * the boolean is not returned because of the need to have the method as async 
         */
        private async void checkEntries()
        {

            string name_pattern = "^([a-z]+[,.]?[ ]?|[a-z]+['-]?)+$",
                email_pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                + "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
                username_pattern = "^[a-zA-Z][a-zA-Z0-9]*$",
                password_pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"; //the username_pattern doesn't let the username start with a digit & shld contain at least 1 letter & 1 number
            Regex name_rgx = new Regex(name_pattern),
                email_rgx = new Regex(email_pattern),
                username_rgx = new Regex(username_pattern),
                password_rgx = new Regex(password_pattern);
            if (fName.Text != null && lName.Text != null && e_Mail.Text != null && userName.Text != null && passWord.Text != null)
            {
                //checking first name 
                if (name_rgx.IsMatch(fName.Text))
                {
                    areValid = true;

                    //checking last name 
                    if (name_rgx.IsMatch(lName.Text))
                    {
                        areValid = true;

                        //checking email format 
                        if (email_rgx.IsMatch(e_Mail.Text))
                        {
                            areValid = true;

                            //checking username format 
                            if (username_rgx.IsMatch(userName.Text))
                            {
                                areValid = true;
                                //checking password format
                                if (password_rgx.IsMatch(passWord.Text))
                                {
                                    areValid = true;
                                }
                                else
                                {
                                    areValid = false;
                                    await DisplayAlert("Alert", "Password should contain at least 1 upper case letter, 1 lower case letter, 1 digit, 1 special character, and minimum 8 in length!", "OK");
                                }
                            }

                            else
                            {
                                areValid = false;
                                await DisplayAlert("Alert", "Username should at least contain one letter and one Number & cannot start with a digit!", "OK");
                            }
                        }
                        else
                        {
                            areValid = false;
                            await DisplayAlert("Alert", "Invalid E-mail Address!", "OK");
                        }
                    }
                    else
                    {
                        areValid = false;
                        await DisplayAlert("Alert", "Last Name can only contain letters!", "OK");
                    }
                }
                else
                {
                    areValid = false;
                    await DisplayAlert("Alert", "First Name can only contain letters!", "OK");
                }
            }
            else
            {
                areValid = false;
                await DisplayAlert("Alert","Please make sure you filled the whole form!", "OK");
            }
        }
    }
}
