using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace LocalUnterTaxiApp
{

    public static class SynchronousSQLite
    {
        public static SQLiteConnection Connection
        {
            get
            {
                return connection;
            }
        }

        private static SQLiteConnection connection;
        private static string databaseFilepath;


        public static void Initialize()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                databaseFilepath = Path.Combine(documentsPath, "PreviousRequests.db");
                Console.WriteLine("this is the document path!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!: "+documentsPath);
                connection = new SQLiteConnection(databaseFilepath);
                connection.CreateTable<Domain.Request>();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("STUFF WHENT WRONG WITH CREATING THE SQLITE DATABASE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            }      
        }



        


        public static void CreateDatabaseAndTables()
        {
            if (File.Exists(databaseFilepath))
                return;

            using (connection)
            {
                connection.CreateTable<Domain.Request>();
            }
        }

        /**
         * This method adds a request to the database
         * 
         */
        public static void addRequest(SQLiteConnection db, string from, string to)
        {
            Domain.Request request = new Domain.Request { From_Location = from, To_Location = to };
            db.Insert(request);
            Console.WriteLine("{0} == {1}", request.Request_ID, request.From_Location, request.To_Location);
        }


        /**
         * This method gets all the requests from the SQLite database
         * 
         */
        public static List<Domain.Request> getRequests()
        {
            //For selecting specific request:
            //TableQuery<Domain.Request> query = connection.Table<Domain.Request>().Where(v => v.Request_ID == id);

            try
            {
                TableQuery<Domain.Request> query = Connection.Table<Domain.Request>();

                List<Domain.Request> list = new List<Domain.Request>();
                //List<Domain.Request> list = connection.Table<Domain.Request>();

                foreach (var request in query)
                {
                    list.Add(request);
                }
                return list;

            }
            catch
            {
                Console.WriteLine("SOMETHING WENT WRONG WITH GETTING THE REQUETS FROM THE TABLE!!!!!!!!!!!!!!!!!!!!!");
                return null;

            }



        }


    }


    
}
