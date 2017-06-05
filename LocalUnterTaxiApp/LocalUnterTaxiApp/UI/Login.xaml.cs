using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalUnterTaxiApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }



        private async void login_btn_Clicked(object sender, EventArgs e)
        {
            await callValidate();
            if (Session.Current_Customer != null)
            {
                Application.Current.MainPage = new NavigationPage(new RequestTaxiForm());
            }
        }

        private async Task callValidate()
        {
            RestService rest_Service = new RestService();
            await rest_Service.ValidateCredentials(username_fld.Text, password_fld.Text);
        }
    }
}