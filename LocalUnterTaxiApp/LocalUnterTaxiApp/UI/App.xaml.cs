using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalUnterTaxiApp.UI;
using Xamarin.Forms;

namespace LocalUnterTaxiApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage( new MainPage())
            {
                BarBackgroundColor = Color.FromRgb(255, 204, 0),//this color is yellow but not too bright 
                BarTextColor=Color.Black,
            };
          
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
