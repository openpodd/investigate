﻿using System.Diagnostics;
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
}
