using System;
using Xamarin.Forms;

namespace Investigate
{
	public class TextLengthValidatorBehavior : Behavior<Entry>
	{
		static BindableProperty MinLengthProperty = BindableProperty.Create(
			"MinLength",
			typeof(int),
			typeof(TextLengthValidatorBehavior),
			0
		);

		public int MinLength
		{
			get { return (int)GetValue(MinLengthProperty); }
			set { SetValue(MinLengthProperty, value); }
		}

		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
			"IsValid",
			typeof(bool),
			typeof(TextLengthValidatorBehavior),
			true
		);

		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			get { return (bool)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		private bool IsOnceCompleted { get; set; }

		protected override void OnAttachedTo(Entry bindable)
		{
			IsOnceCompleted = false;
			bindable.Unfocused += HandlerCompleted;
			bindable.TextChanged += HandlerTextChanged;
		}

		void HandlerTextChanged(object sender, TextChangedEventArgs e)
		{
			ValidateLength((Entry)sender);
		}

		void HandlerCompleted(object sender, EventArgs e)
		{
			IsOnceCompleted = true;
			ValidateLength((Entry)sender);
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.Unfocused -= HandlerCompleted;
			bindable.TextChanged -= HandlerTextChanged;
		}

		public void ValidateLength(Entry entry)
		{
			IsValid = !IsOnceCompleted || (entry.Text != null && entry.Text.Length >= MinLength);
			entry.TextColor = IsValid ? Color.Default : Color.Red;
		}

		public void ResetValidation()
		{
			IsOnceCompleted = false;
			IsValid = true;
		}
	}
}
