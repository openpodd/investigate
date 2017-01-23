using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{
		PoddService service;
		ObservableCollection<SearchItem> reports;

		public ReportSelectionPage()
		{
			InitializeComponent();
			service = new PoddService();
			reports = new ObservableCollection<SearchItem>();
			reportListView.ItemsSource = reports;
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			Settings.Token = "";

			Navigation.InsertPageBefore(new LoginPage(), this);
			await Navigation.PopAsync();
		}

		async void OnSearchButtonClicked(object sender, EventArgs e)
		{
			reports.Clear();

			var results = await service.Search(new SearchRequest());
			resultLabel.Text = String.Format("Result: {0} items", results.Count);
			foreach (SearchItem item in results.results)
			{
				reports.Add(item);
			}
		}
	}
}
