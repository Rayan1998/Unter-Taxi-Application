using System;
using System.Collections.Generic;
using System.Text;

namespace LocalUnterTaxiApp.Domain
{
   public class Customer
    {
        private int customerID;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string fName;

        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }

        private string lName;

        public string LName
        {
            get { return lName; }
            set { lName = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string  password;

        public string  Password
        {
            get { return password; }
            set { password = value; }
        }

        private string phoneNb;

        public string PhoneNB
        {
            get { return phoneNb; }
            set { phoneNb = value; }
        }

        private string preffered_Brand;

        public string Preffered_Brand
        {
            get { return preffered_Brand; }
            set { preffered_Brand = value; }
        }

        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public Customer(string f_name, string l_name, string e_mail,string username, string pass, string phone_NB, string preferred_brand)
        {
            FName = f_name;
            LName = l_name;
            Email=  e_mail;
            Username = username;
            Password = pass;
            PhoneNB = phone_NB;
            Preffered_Brand = preferred_brand;
            
        }
    }
}
