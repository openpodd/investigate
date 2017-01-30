using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Investigate
{
	public class DateCell : ViewCell
	{
	    public static readonly BindableProperty DateProperty = BindableProperty.Create(
	        "Date",
	        typeof(DateTime),
	        typeof(DateCell),
	        System.DateTime.Now,
	        BindingMode.TwoWay);

	    public static readonly BindableProperty LabelProperty = BindableProperty.Create(
	        "Label",
	        typeof(string),
	        typeof(DateCell),
	        "วันที่");

	    public DateTime Date
	    {
	        get { return (DateTime) GetValue(DateProperty);  }
	        set
	        {
	            SetValue(DateProperty, value);
	        }
	    }

	    public string Label
	    {
	        get { return (string) GetValue(LabelProperty);  }
	        set
	        {
	            SetValue(LabelProperty, value);
	        }
	    }

	    public DateCell() : base()
	    {
	        var layout = new StackLayout()
	        {
	            Orientation = StackOrientation.Horizontal,
	            HorizontalOptions = LayoutOptions.FillAndExpand
	        };

	        layout.Children.Add(new Label()
	        {
	            Text = Label
	        });

	        layout.Children.Add(new DatePicker
	        {
	            Format = "dd MMMM yyyy",
	            HorizontalOptions = LayoutOptions.End,
	            Date = Date
	        });

	        this.View = layout;
	    }
	}
}
