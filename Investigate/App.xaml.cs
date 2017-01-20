using Xamarin.Forms;

namespace Investigate
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			if (Settings.IsLogin())
			{
				MainPage = new NavigationPage(new InvestigatePage());
			}
			else 
			{
				MainPage = new NavigationPage(new LoginPage());
			}

		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
