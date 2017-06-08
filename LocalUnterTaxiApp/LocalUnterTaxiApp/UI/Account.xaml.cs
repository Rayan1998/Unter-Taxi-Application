using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalUnterTaxiApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {


        public Account()
        {
            InitializeComponent();
            userName.Text = Session.Current_Customer.Username;
            passWord.Text = Session.Current_Customer.Password;
            e_Mail.Text = Session.Current_Customer.Email;

        }

        private void Save_Btn_Clicked(object sender, EventArgs e)
        {

        }

        private void logout_Btn_Clicked(object sender, EventArgs e)
        {
            Session.Current_Customer = null;
            Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromRgb(255, 204, 0),//this color is yellow but not too bright 
                BarTextColor = Color.Black,
            };
        }

        private async void deactivate_Btn_Clicked(object sender, EventArgs e)
        {
            //TODO : Test
            await callDeactivate();
        }

        private async Task callDeactivate()
        {
            RestService rest = new RestService();
            await rest.DeactivateAccount(Session.Current_Customer.CredentialsID);
        }
    }
}