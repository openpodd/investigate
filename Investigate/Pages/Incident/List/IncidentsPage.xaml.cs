using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
			var incident = (IncidentResult) e.Item;
            var page = new IncidentMasterDetailPage(ReportInvestigate.Id, incident.Uuid);
	        Debug.WriteLine($"Loading incident UUID: {incident.Uuid}");
	        Navigation.PushModalAsync(page, true);
	    }

	    void OnClickAddButton(object sender, EventArgs e)
	    {
			Debug.WriteLine("add incident by reportInvestigateId {0}", ReportInvestigate.Id);
	        var formPage = new IncidentFormPage(ReportInvestigate.Id, "");
	        Navigation.PushAsync(formPage, true);
	    }
	}

    public class IncidentListViewModel : BaseViewModel
    {
        ReportInvestigate ReportInvestigateInstance { get; set; }
        public IEnumerable<IncidentResult> Incidents { get; set; }


        /**
         * to use async method in constructor
         * we must use this pattern to create instance
         */
        public static async Task<IncidentListViewModel> create(long reportInvestigateId)
        {
            var instance = new IncidentListViewModel();
            instance.Incidents = await App.Repository.GetIncidentListByReportInvestigateId(reportInvestigateId);
            Debug.WriteLine(string.Format("Done loading from db total {0} rows", instance.Incidents.Count()));
            return instance;
        }

        private IncidentListViewModel()
        {
        }
    }
}
