using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{
		public ReportSelectionPage()
		{
			InitializeComponent();
			var closeAction = new Action(ClosePage);
			BindingContext = new ReportSelectionViewModel(closeAction);
		}

		async public void ClosePage()
		{
			await Navigation.PopAsync();
		}
	}
}
