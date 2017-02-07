using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
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


    public class IncidentFormViewModel : BaseViewModel
    {
        public long ReportInvestigateId { get; set; }
        public Incident Incident { get; set; }


        public ICommand SaveCommand { get; private set; }
        public Action SaveSuccessAction { get; set; }

        public static async Task<IncidentFormViewModel> Create(long reportInvestigateId, string uuid)
        {
            var instance = new IncidentFormViewModel();
            instance.ReportInvestigateId = reportInvestigateId;

            if (string.IsNullOrEmpty(uuid))
            {
                instance.Incident = new Incident();
                instance.Incident.ReportInvestigateId = reportInvestigateId;
                Debug.WriteLine($"New Incident UUID: {instance.Incident.Uuid} : {instance.Incident.Village} {instance.Incident.HouseNumber} {instance.Incident.HouseOwnerName}");
            }
            else
            {
                instance.Incident = await App.Repository.GetIncidentByUUID(uuid);
                Debug.WriteLine($"Existing Incident UUID: {instance.Incident.Uuid} : {instance.Incident.Village} {instance.Incident.HouseNumber} {instance.Incident.HouseOwnerName}");
            }

            return instance;
        }

        public IncidentFormViewModel()
        {
            SaveCommand = new Command(Save);
        }

        void Save()
        {
            Debug.WriteLine("SaveCommand called");

            Incident.UpdatedAt = DateTimeOffset.Now;
            App.Repository.InsertOrUpdateAsync(Incident);

            Debug.WriteLine($"Updated Incident : {Incident.Village} {Incident.HouseNumber} {Incident.HouseOwnerName}");
            Debug.WriteLine($"Saved : incident UUID : {Incident.Uuid}");

            SaveSuccessAction?.Invoke();
        }
    }
}
