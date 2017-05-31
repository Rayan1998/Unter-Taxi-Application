using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LocalUnterTaxiApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class previousRequestsPage : ContentPage
	{
		public previousRequestsPage ()
		{
			InitializeComponent ();
            BindingContext = new RequestsViewModel();
        }
	}





    class RequestsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Domain.Request> Requests { get; }
        public ObservableCollection<Grouping<string, Domain.Request>> RequestsGrouped { get; }


        public RequestsViewModel()
        {
            //TODO: set the requests!!

            Requests = new ObservableCollection<Domain.Request>()
             {
                 new Domain.Request(){Customer_ID = 1, From_Location = "from", To_Location = "to"},
                 new Domain.Request(){Customer_ID = 2, From_Location = "from2", To_Location = "to2"},
                 new Domain.Request(){Customer_ID = 3, From_Location = "", To_Location = ""},
                 new Domain.Request(){Customer_ID = 4, From_Location = "", To_Location = ""},
                 new Domain.Request(){Customer_ID = 5, From_Location = "", To_Location = ""},
                 new Domain.Request(){Customer_ID = 6, From_Location = "", To_Location = ""},
                 new Domain.Request(){Customer_ID = 7, From_Location = "from7", To_Location = "to7"},
            };



            /*Items = new ObservableCollection<Item>(new[]
            {
                new Item { Text = "Baboon", Detail = "Africa & Asia" },
                new Item { Text = "Capuchin Monkey", Detail = "Central & South America" },
                new Item { Text = "Blue Monkey", Detail = "Central & East Africa" },
                new Item { Text = "Squirrel Monkey", Detail = "Central & South America" },
                new Item { Text = "Golden Lion Tamarin", Detail= "Brazil" },
                new Item { Text = "Howler Monkey", Detail = "South America" },
                new Item { Text = "Japanese Macaque", Detail = "Japan" },
            });
*/
            var sorted = from request in Requests
                         orderby request.Text
                         group request by request.ToString() into requestGroup
                         select new Grouping<string, Domain.Request>(requestGroup.Key, requestGroup);

            RequestsGrouped = new ObservableCollection<Grouping<string, Domain.Request>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;

        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                //((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

       

        
        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> requests)
            {
                Key = key;
                foreach (var request in requests)
                    this.Requests.Add(request);
            }
        }
    }
}