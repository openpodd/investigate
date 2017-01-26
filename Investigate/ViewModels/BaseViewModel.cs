using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Investigate
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public INavigation Navigation;
	    protected ReportInvestigate _reportInvestigate;

	    public BaseViewModel()
		{
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
