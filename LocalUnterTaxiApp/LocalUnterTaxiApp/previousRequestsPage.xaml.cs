using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class previousRequestsPage : ContentPage
	{

        public string testRequest;

		public previousRequestsPage ()
		{
            

			InitializeComponent ();

            BindingContext = new PreviousRequestsViewModel();

        }
    }

    public class PreviousRequestsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Domain.Request> items;
        public ObservableCollection<Domain.Request> Items
        {
            get { return items; }
            set
            {

                items = value;
                OnPropertyChanged("Items");
            }
        }


        public PreviousRequestsViewModel()
        {

            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            Items = new ObservableCollection<Domain.Request>() {
                new Domain.Request()
                {
                    Request_ID = 1,
                    From_Location = "From somewhere",
                    To_Location = "to somewhere"
                },
                  new Domain.Request()
                {
                    Request_ID = 2,
                    From_Location = "From somewhere else",
                    To_Location = "to somewhere else"
                },
            };

            SynchronousSQLite.Initialize();

            SynchronousSQLite.addRequest(SynchronousSQLite.Connection, "sql lite saved from", "sql lite saved to");

            List<Domain.Request> previousRequestsList = SynchronousSQLite.getRequests();

            foreach (Domain.Request request in previousRequestsList)
            {
                Items.Add(request);
            }


            //Items = SynchronousSQLite.getRequests();


        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}