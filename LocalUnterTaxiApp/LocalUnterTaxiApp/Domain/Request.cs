using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace LocalUnterTaxiApp.Domain
{
    public class Request
    {
        
        
        private int request_ID;

        [PrimaryKey, AutoIncrement]
        public int Request_ID
        {
            get { return request_ID; }
            set { request_ID = value; }
        }



        private int customer_ID;

        public int Customer_ID
        {
            get { return customer_ID; }
            set { customer_ID = value; }
        }

        private string from_Location;

        public string From_Location
        {
            get { return from_Location; }
            set { from_Location = value; }
        }

        private string  to_Location;

        public string  To_Location 
        {
            get { return to_Location; }
            set { to_Location = value; }
        }

        public Request( int cust_ID, string from_loc, string to_loc)
        {
            this.customer_ID = cust_ID;
            this.from_Location = from_loc;
            this.to_Location = to_loc;
        }

        public Request()
        {

        }

    }
}
