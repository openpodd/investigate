using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Investigate
{
	public partial class IncidentAnimalStatListPage : ContentPage
	{
	    public IncidentAnimalStatListViewModel ViewModel { get; private set; }
	    public string IncidentUuid { get; private set; }

	    public IncidentAnimalStatListPage(string uuid)
		{
			InitializeComponent();
		    IncidentUuid = uuid;

		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        ViewModel = await IncidentAnimalStatListViewModel.Create(IncidentUuid);
	        BindingContext = ViewModel;
	    }

	    void OnClickNewStatButton(object sender, EventArgs e)
	    {
	        var page = new IncidentAnimalStatFormPage(IncidentUuid, "");
	        Navigation.PushAsync(page, true);
	    }

	    void OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var item = (IncidentAnimalStat) e.Item;
	        Debug.WriteLine($"IncidentFormPage:OnItemTapped called with UUID : {item.Uuid}");
	        var page = new IncidentAnimalStatFormPage(IncidentUuid, item.Uuid);
	        Navigation.PushAsync(page, true);
	    }
	}

    public class IncidentAnimalStatListViewModel
    {
        public Incident Incident { get; set; }
        public IEnumerable<IncidentAnimalStat> IncidentAnimalStatList { get; set; }

        public static async Task<IncidentAnimalStatListViewModel> Create(string uuid)
        {
            var instance =
                new IncidentAnimalStatListViewModel
                {
                    Incident = await App.Repository.GetIncidentByUUID(uuid),
                    IncidentAnimalStatList = await App.Repository.GetIncidentAnimalStatListByIncidentUuid(uuid)
                };
            return instance;
        }

    }
}
