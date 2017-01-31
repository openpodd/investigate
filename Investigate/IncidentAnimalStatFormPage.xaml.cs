using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentAnimalStatFormPage : ContentPage
	{
	    private string _incidentUuid;
	    private string _incidenAnimalStatUuid;

		public IncidentAnimalStatFormPage(string incidentUuid, string incidenAnimalStatUuid)
		{
			InitializeComponent();
		    _incidentUuid = incidentUuid;
		    _incidenAnimalStatUuid = incidenAnimalStatUuid;
		}

	    protected override async void OnAppearing()
	    {
	        var viewModel = await IncidentAnimalStatFormViewModel.Create(_incidentUuid, _incidenAnimalStatUuid);
	        viewModel.SaveSuccessAction = SaveSuccess;
	        BindingContext = viewModel;
	        base.OnAppearing();
	    }

	    void SaveSuccess()
	    {
	        Navigation.PopAsync(true);
	    }
	}
}
