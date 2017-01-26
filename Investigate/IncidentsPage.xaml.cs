using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentsPage : ContentPage
	{
		public long ReportInvestigateId { get; set; }

		public IncidentsPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			BindingContext = new IncidentListViewModel(ReportInvestigateId);
			base.OnAppearing();
		}

	    void OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var incident = (Incident) e.Item;
            var formPage = new IncidentFormPage(incident);
	        Navigation.PushAsync(formPage, true);
	    }
	}
}
