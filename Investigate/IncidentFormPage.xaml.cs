using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentFormPage : ContentPage
	{
	    private long _reportInvestigateId;
	    private readonly string _incidentUuid;

	    public IncidentFormPage(long reportInvestigateId) : this(reportInvestigateId, "")
	    {
		}

	    public IncidentFormPage(long reportInvestigateId, string uuid)
		{
		    _incidentUuid = uuid;
		    _reportInvestigateId = reportInvestigateId;
		    InitializeComponent();
		}

	    protected async override void OnAppearing()
	    {
			var viewModel = await IncidentFormViewModel.Create(_reportInvestigateId, _incidentUuid);
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
