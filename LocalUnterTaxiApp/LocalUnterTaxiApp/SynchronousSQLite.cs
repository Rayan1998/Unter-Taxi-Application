using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace LocalUnterTaxiApp
{
    /**
     * This class provices connection to the SQLite database
     * Besides this, it equip the developer with methods for adding requests to SQLite table and viewing all the locally saved requests.
     * 
     * The class and its methods are static ...
     * 
     * Consider using SqliteOpenHelper??? 
     */
    public static class SynchronousSQLite
    {
        /**
         * We want to be able to get the connection to the database at all times, and not having to call 
         * initialize every time we need to contact the database. 
         * Therefore we check if the connection is initialized, and if not, we run the initialize.
         * 
         * This way, whenever we need to do anything in the SQLite, we simply ask for the connection.
         * 
         */ 
        public static SQLiteConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    Initialize();
                }
                return connection;
            }
        }

        private static SQLiteConnection connection;
        private static string databaseFilepath;


        /**
         * This method initializes the SQLite database and table
         * This needs to be called everytime 
         */ 
        public static void Initialize()
        {
            try
            {
                //We get the path from the device:
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //We set the path for the database to the given path combined with the name of db:
                databaseFilepath = Path.Combine(documentsPath, "PreviousRequests.db");

                Console.WriteLine("Document path: "+documentsPath); //For debugging (and fun)

                //We create a connection to the database at the location. If the database does not exist, this will create it
                connection = new SQLiteConnection(databaseFilepath);
                //We create the table used for holding requets:
                connection.CreateTable<Domain.Request>();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("STUFF WHENT WRONG WITH CREATING THE SQLITE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }      
        }


        /**
         * This method adds a request to the database
         * 
         */
        public static void addRequest(SQLiteConnection db, string from, string to)
        {
            //We create the request:
            Domain.Request request = new Domain.Request { From_Location = from, To_Location = to };
            //We insert it into the database:
            db.Insert(request);


            //Console.WriteLine("{0} == {1}", request.Request_ID, request.From_Location, request.To_Location);
        }




        /**
         * This method gets all the requests from the SQLite database in a list
         * 
         */
        public static List<Domain.Request> getRequests()
        {
            //Syntax for selecting specific request:
            //TableQuery<Domain.Request> query = connection.Table<Domain.Request>().Where(v => v.Request_ID == id);

            try
            {
                //We get the requests from the database:
                TableQuery<Domain.Request> query = Connection.Table<Domain.Request>();

                //We create a list of requests:
                List<Domain.Request> list = new List<Domain.Request>();

                //Loop puts requets from SQLite into the list:
                foreach (var request in query)
                {
                    list.Add(request);
                }
                return list;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("SOMETHING WENT WRONG WITH GETTING THE REQUETS FROM THE TABLE!!!!!!!!!!!!!!!!!!!!!"); //For debugging
                return null;

            }
        }
    }    
}
