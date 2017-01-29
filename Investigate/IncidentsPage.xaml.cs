﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentsPage : ContentPage
	{
		public ReportInvestigate ReportInvestigate { get; set; }

		public IncidentsPage()
		{
			InitializeComponent();
		    addButton.Clicked += OnClickAddButton;
		}

		protected async override void OnAppearing()
		{
			Debug.WriteLine($"[IncidentsPage] ReportInvestigateId = {ReportInvestigate.Id}");
			BindingContext = await IncidentListViewModel.create(ReportInvestigate.Id);
		    base.OnAppearing();
		}

	    void OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var incident = (Incident) e.Item;
            var formPage = new IncidentFormPage(ReportInvestigate.Id, incident.Uuid);
	        Navigation.PushAsync(formPage, true);
	    }

	    void OnClickAddButton(object sender, EventArgs e)
	    {
			Debug.WriteLine("add incident by reportInvestigateId {0}", ReportInvestigate.Id);
	        var formPage = new IncidentFormPage(ReportInvestigate.Id, "");
	        Navigation.PushAsync(formPage, true);
	    }
	}
}