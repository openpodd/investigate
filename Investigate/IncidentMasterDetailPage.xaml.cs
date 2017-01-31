using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentMasterDetailPage : MasterDetailPage
	{
        private IncidentMasterDetailViewModel _viewModel;

	    public IncidentMasterDetailPage(long reportInvestigateId, string incidentUuid)
	    {
	        InitializeComponent();

	        _viewModel = new IncidentMasterDetailViewModel()
	        {
	            ReportInvestigateId = reportInvestigateId,
	            IncidentUuid = incidentUuid,
	            ChangeDetailPageAction = ChangeDetailPage
	        };
	        masterPage.ViewModel = _viewModel;
	        // Init first item as default page.
	        _viewModel.ChangePageCommand.Execute(_viewModel.PageItems[0]);
	    }

	    private void ChangeDetailPage(Page page)
	    {
	        Detail = new NavigationPage(page);
	        IsPresented = false;
	    }
	}
}
