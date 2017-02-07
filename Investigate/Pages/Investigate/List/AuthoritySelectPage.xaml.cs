﻿using System.Collections.ObjectModel;
 using System.Diagnostics;
 using System.Linq;
 using System.Threading.Tasks;
 using Xamarin.Forms;

namespace Investigate
{
	public partial class AuthoritySelectPage : ContentPage
	{

	    private readonly AuthoritySelectViewModel _viewModel;

	    public AuthoritySelectPage(AuthoritySelectViewModel viewModel)
		{
		    _viewModel = viewModel;
		    InitializeComponent();
		    Items.ItemTapped += (sender, args) =>
		    {
		        var authorityItem = (AuthorityItem) args.Item;
		        Select(authorityItem);
		    };
		    Debug.WriteLine("initialize complete");
		}

	    protected override void OnAppearing()
	    {
			BindingContext = _viewModel;
			base.OnAppearing();
	        _viewModel.Search();
	    }

	    public void Select(AuthorityItem item)
	    {
	        _viewModel.TaskCompletionSource.SetResult(item);
	    }
	}


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
