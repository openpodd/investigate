using System;

using System.Threading.Tasks;
using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{
		public ReportSelectionPage()
		{
			InitializeComponent();
			var closeAction = new Action(ClosePage);
			BindingContext = new ReportSelectionViewModel
			{
				CloseAction = closeAction,
				PoddService = new PoddService()
			};
		}

		async public void ClosePage()
		{
			await Navigation.PopAsync();
		}
	}
}
