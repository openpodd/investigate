using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentsPage : ContentPage
	{
		public long ReportInvestigateId { get; set; }

		public IncidentsPage()
		{
			InitializeComponent();
		    addButton.Clicked += OnClickAddButton;
		}

		protected override void OnAppearing()
		{
		    Debug.WriteLine($"[IncidentsPage] ReportInvestigateId = {ReportInvestigateId}");
		    BindingContext = new IncidentListViewModel(ReportInvestigateId);
		    base.OnAppearing();
		}

	    void OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var incident = (Incident) e.Item;
            var formPage = new IncidentFormPage(ReportInvestigateId, incident.Uuid);
	        Navigation.PushAsync(formPage, true);
	    }

	    void OnClickAddButton(object sender, EventArgs e)
	    {
	        var formPage = new IncidentFormPage(ReportInvestigateId);
	        Navigation.PushAsync(formPage, true);
	    }
	}
}
