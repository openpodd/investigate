using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
    public class IncidentMasterDetailViewModel : BaseViewModel
    {
        public long ReportInvestigateId { get; set; }
        public string IncidentUuid { get; set; }

        public List<MasterPageItem> PageItems { get; set; }
        public ICommand ChangePageCommand { get; private set; }
        public Action<Page> ChangeDetailPageAction { get; set; }

        public IncidentMasterDetailViewModel()
        {
            ChangePageCommand = new Command<MasterPageItem>(ChangePage);
            PageItems = new List<MasterPageItem>
            {
                new MasterPageItem()
                {
                    Title = "Location",
                    Type = PageType.Detail
                },
                new MasterPageItem()
                {
                    Title = "Number of Sick/Death",
                    Type = PageType.AnimalStat
                },
                new MasterPageItem()
                {
                    Title = "ตัวอย่างซาก",
                    Type = PageType.Sample
                }
            };
        }

        public void ChangePage(MasterPageItem pageItem)
        {
            if (pageItem == null) return;

            object page = null;
            switch (pageItem.Type)
            {
                case PageType.Detail:
                    page = new IncidentFormPage(ReportInvestigateId, IncidentUuid);
                    break;
                case PageType.AnimalStat:
                    page = new IncidentAnimalStatListPage(IncidentUuid);
                    break;
                case PageType.Sample:
                    page = new IncidentAnimalStatListPage(IncidentUuid);
                    break;
                default:
                    page = new IncidentFormPage(ReportInvestigateId, IncidentUuid);
                    break;
            }

            ChangeDetailPageAction?.Invoke((Page) page);
        }
    }
}