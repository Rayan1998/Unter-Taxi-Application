using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LocalUnterTaxiApp.Domain
{
    public class Request
    {

        [PrimaryKey, AutoIncrement]
        public int Request_ID
        {
            get;
            set;
        }

        public int Customer_ID
        {
            get;
            set;
        }

        private int credentials_ID;


        public int Credentials_ID
        {
            get { return credentials_ID; }
            set { credentials_ID = value; }
        }


        public string From_Location
        {
            get;
            set;
        }


        public string To_Location
        {
            get;
            set;
        }

        public Request(int cust_ID, string from_loc, string to_loc)
        {
            this.Customer_ID = cust_ID;
            this.From_Location = from_loc;
            this.To_Location = to_loc;
        }

        /**
         * Empty constructor used for inputting requets into Observable collection
         */
        public Request()
        {

        }

    }
}
