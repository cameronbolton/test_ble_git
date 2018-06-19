using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using nexus.protocols.ble;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace test_ble_git
{
	public partial class App : Application
	{
		public App (IBluetoothLowEnergyAdapter adapter)
		{
			InitializeComponent();

			MainPage = new MainPage(adapter);
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
