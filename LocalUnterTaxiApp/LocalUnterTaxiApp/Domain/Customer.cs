using System;
using System.Collections.Generic;
using System.Text;

namespace LocalUnterTaxiApp.Domain
{
   public class Customer
    {

        public int CustomerID
        {
            get;
            set; 
        }


        public string FName
        {
            get;
            set;
        }


        public string LName
        {
            get;
            set;
        }


        public string Email
        {
            get;
            set;
        }


        public string  Password
        {
            get;
            set;
        }

        private string phoneNb;

        public string PhoneNB
        {
            get;
            set;
        }


        public string Preffered_Brand
        {
            get;
            set;
        }


        public int Priority
        {
            get;
            set;
        }


        public string Username
        {
            get;
            set;
        }

        /**
         * Constructor
         */ 
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

        /**
         * Empty constructor
         */ 
        public Customer(){}
    }
}
