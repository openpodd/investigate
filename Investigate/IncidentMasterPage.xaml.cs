using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentMasterPage : ContentPage
	{
	    public IncidentMasterDetailViewModel ViewModel { get; set; }

	    public IncidentMasterPage()
		{
		    InitializeComponent();
		    listView.ItemSelected += OnItemSelected;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        listView.ItemsSource = ViewModel.PageItems;
	    }

	    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var item = (MasterPageItem) e.SelectedItem;
	        listView.SelectedItem = null;
	        ViewModel.ChangePageCommand.Execute(item);
	    }
	}
}
