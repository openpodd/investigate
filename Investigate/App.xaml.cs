using System.Linq;
using Xamarin.Forms;

namespace Investigate
{
	public partial class App : Application
	{

		static IRepository repository;
		public static IRepository Repository
		{
			get
			{
				if (repository == null)
				{
					repository = new RepositoryImpl();
				}
				return repository;
			}
		}

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

			Repository.initTableAsync();
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
