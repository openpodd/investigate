using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CoreGraphics;

using Investigate.iOS;
using Investigate;

[assembly: ExportRenderer(typeof(NoCapEntry), typeof(NoCapEntryRenderer))]
namespace Investigate.iOS
{
	public class NoCapEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.SpellCheckingType = UITextSpellCheckingType.No;
				Control.AutocorrectionType = UITextAutocorrectionType.No;
				Control.AutocapitalizationType = UITextAutocapitalizationType.None;
			}
		}
	}
}
