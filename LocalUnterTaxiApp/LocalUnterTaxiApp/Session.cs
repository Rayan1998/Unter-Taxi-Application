using System;
using System.Collections.Generic;
using System.Text;
using LocalUnterTaxiApp.Domain;

namespace LocalUnterTaxiApp
{
    class Session
    {

        private static Customer current_Customer;

        public static Customer Current_Customer
        {
            get { return current_Customer; }
            set { current_Customer = value; }
        }

        private Session()
        {
        }

    }
}
