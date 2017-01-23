using System;
using Xamarin.Forms;

namespace Investigate
{
	public class TextEntry : Entry
	{
		public new event EventHandler Completed;
		public Entry Next { get; set; }

		public const string ReturnKeyPropertyName = "ReturnKeyType";

		public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
			propertyName: ReturnKeyPropertyName,
			returnType: typeof(ReturnKeyType),
			declaringType: typeof(TextEntry),
			defaultValue: ReturnKeyType.Done,
			defaultBindingMode: BindingMode.OneWay
		);

		public ReturnKeyType ReturnKeyType
		{
			get { return (ReturnKeyType)GetValue(ReturnKeyTypeProperty); }
			set { SetValue(ReturnKeyTypeProperty, value); }
		}

		public TextEntry()
		{
		}

		public void InvokeCompleted()
		{
			if (this.Completed != null)
			{
				this.Completed.Invoke(this, null);
			}

			GotoNext();
		}

		private void GotoNext()
		{
			if (this.Next != null)
			{
				this.Next.Focus();
			}
		}
	}
}
