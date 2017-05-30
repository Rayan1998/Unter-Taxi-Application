using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Register_Btn_Clicked(object sender, EventArgs e)
        {
            string f_Name, l_Name, e_mail, username, password, preferred_Brand;//, phoneNb, ;
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
            if (fName.Text != "" && lName.Text != "" && e_Mail.Text != "" && userName.Text != "" && passWord.Text != "")
            {
                //checking first name 
                if (name_rgx.IsMatch(fName.Text))
                {
                    f_Name = fName.Text;
                }
                else
                {
                    await DisplayAlert("Alert", "First Name can only contain letters!", "OK");
                }
                //checking last name 
                if (name_rgx.IsMatch(lName.Text))
                {
                    l_Name = lName.Text;
                }
                else
                {
                    await DisplayAlert("Alert", "Last Name can only contain letters!", "OK");
                }
                //
                if (email_rgx.IsMatch(e_Mail.Text))
                {
                    e_mail = e_Mail.Text;
                }
                else
                {
                    await DisplayAlert("Alert", "Invalid E-mail Address!", "OK");
                }
                if (username_rgx.IsMatch(userName.Text))
                {
                    username = userName.Text;
                }
                else
                {
                    await DisplayAlert("Alert", "Username should at least contain one letter and one Number & cannot start with a digit!", "OK");
                }
                if (password_rgx.IsMatch(passWord.Text))
                {
                    password = passWord.Text;
                }
                else
                {
                    await DisplayAlert("Alert", "Password should contain at least 1 upper case letter, 1 lower case letter, 1 digit, 1 special character, and minimum 8 in length!", "OK");
                }
            }
        }
    }
}