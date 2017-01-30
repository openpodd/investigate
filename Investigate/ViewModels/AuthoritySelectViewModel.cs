using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Investigate
{
    public class AuthoritySelectViewModel : BaseViewModel
    {

        public IPoddService PoddService { set; get; }

        public TaskCompletionSource<AuthorityItem> TaskCompletionSource { get; set; }

        public string AuthoritySearchText { set; get; }
        public ObservableCollection<AuthorityItem> AuthorityItems { get; set; }

        public Command BackCommand { get; set; }
        public Command SearchCommand { get; }

        public AuthoritySelectViewModel()
        {
            TaskCompletionSource = new TaskCompletionSource<AuthorityItem>();
            AuthorityItems = new ObservableCollection<AuthorityItem>();
            SearchCommand = new Command(Search);
        }

        public async void Search()
        {
            Debug.WriteLine("Search Begin");

            var items = await PoddService.GetAuthorities();

			if (string.IsNullOrEmpty(AuthoritySearchText))
            {
                AuthorityItems = new ObservableCollection<AuthorityItem>(items.Items);
            }
            else
            {
                AuthorityItems = new ObservableCollection<AuthorityItem>(from i in items.Items
                    where i.Name.Contains(AuthoritySearchText)
                    select i);
            }

            OnPropertyChanged("AuthorityItems");
        }
    }
}