using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Investigate
{
	public partial class ReportSelectionPage : ContentPage
	{


	    private readonly ReportSelectionViewModel _viewModel;
		public ReportSelectionPage()
		{
			InitializeComponent();

			_viewModel = new ReportSelectionViewModel
			{
				DoneReportSelection = new Action<HashSet<SearchItem>>(ClosePage),
			    ShowAuthoritySelectPageAction = new Action(ShowAuthoritySelectPage),
				PoddService = new PoddService()
			};

		    BindingContext = _viewModel;
		}

		async public void ClosePage(HashSet<SearchItem> reports)
		{
			// Create investigate here.
			foreach (var item in reports)
			{
				var investigate = new ReportInvestigate
				{
					ReportId = item.Id,
					ReportDate = item.Date,
					ReportIncidentDate = new DateTimeOffset(item.IncidentDate),
					ReportTypeName = item.ReportTypeName,
					ReportStateName = item.ReportStateName,
					ReportAdministrationAreaName = item.AdministrationAreaName,
					ReportCreateByName = item.CreateByName,
					ReportCreateByContact = item.CreateByContact,
					ReportCreateByTelephone = item.CreateByTelephone,
					ReportRendererFormData = item.RendererFormData
				};
				await App.Repository.CreateAsync(investigate);
			}

		    await Navigation.PopAsync(true);
		}

	    public async void ShowAuthoritySelectPage()
	    {
			var viewModel = new AuthoritySelectViewModel()
			{
			    PoddService = new PoddService(),
			    BackCommand = new Command(() => Navigation.PopModalAsync())
			};
	        await Navigation.PushModalAsync(new AuthoritySelectPage(viewModel));
	        var item = await viewModel.TaskCompletionSource.Task;
            _viewModel.SetAuthorityFilter(item);
	        await Navigation.PopModalAsync();
	        Debug.WriteLine(item.Name);
	    }
	}
}
