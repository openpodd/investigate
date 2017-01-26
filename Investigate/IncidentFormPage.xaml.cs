using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentFormPage : ContentPage
	{
	    private readonly Incident _incident;

		public IncidentFormPage(Incident incident)
		{
		    _incident = incident;
			InitializeComponent();
		}

	    protected override void OnAppearing()
	    {
	        BindingContext = new IncidentFormViewModel(_incident);
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
	            formHorizontal.IsVisible = true;
	            formVertical.IsVisible = false;
	        }
	        else
	        {
	            formHorizontal.IsVisible = false;
	            formVertical.IsVisible = true;
	        }
	    }
	}
}
