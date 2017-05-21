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


        public Customer(string f_name, string l_name, string e_mail, string pass, string phone_NB, string preferred_brand)
        {
            fName = f_name;
            lName = l_name;
            email=  e_mail;
            password = pass;
            phoneNb = phone_NB;
            preffered_Brand = preferred_brand;
            
        }
    }
}
