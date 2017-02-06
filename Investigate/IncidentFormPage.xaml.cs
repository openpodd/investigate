using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentFormPage : ContentPage
	{
	    public long ReportInvestigateId;
	    public readonly string IncidentUuid;

	    public IncidentFormPage(long reportInvestigateId) : this(reportInvestigateId, "")
	    {
		}

	    public IncidentFormPage(long reportInvestigateId, string uuid)
		{
		    IncidentUuid = uuid;
		    ReportInvestigateId = reportInvestigateId;
		    InitializeComponent();
		}

	    protected async override void OnAppearing()
	    {
			var viewModel = await IncidentFormViewModel.Create(ReportInvestigateId, IncidentUuid);
			viewModel.SaveSuccessAction = SaveSuccess;
			BindingContext = viewModel;
			base.OnAppearing();
	    }

	    protected override void OnSizeAllocated(double width, double height)
	    {
	        base.OnSizeAllocated(width, height);
	        CheckOrientation(width, height);
	    }

	    void CheckOrientation(double width, double height)
	    {
	        if (width > height)
	        {
	            FormHorizontal.IsVisible = true;
	            FormVertical.IsVisible = false;
	        }
	        else
	        {
	            FormHorizontal.IsVisible = false;
	            FormVertical.IsVisible = true;
	        }
	    }

	    void SaveSuccess()
	    {
	        Navigation.PopAsync(true);
	    }


	}
}
