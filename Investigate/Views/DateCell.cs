/**
Copyright 2016 opendream.co.th

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

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
