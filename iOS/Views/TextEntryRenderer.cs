using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

using Investigate.iOS;
using Investigate;

[assembly: ExportRenderer(typeof(TextEntry), typeof(TextEntryRenderer))]
namespace Investigate.iOS
{
	public class TextEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			TextEntry entry = (TextEntry)this.Element;

			if (Control != null)
			{
				Control.SpellCheckingType = UITextSpellCheckingType.No;
				Control.AutocorrectionType = UITextAutocorrectionType.No;
				Control.AutocapitalizationType = UITextAutocapitalizationType.None;

				if (entry != null)
				{
					SetReturnType(entry);

					Control.ShouldReturn += (UITextField tf) =>
					{
						entry.InvokeCompleted();
						return true;
					};
				}
			}
		}

		private void SetReturnType(TextEntry entry)
		{
			ReturnKeyType type = entry.ReturnKeyType;

			switch (type)
			{
				case ReturnKeyType.Go:
					Control.ReturnKeyType = UIReturnKeyType.Go;
					break;
				case ReturnKeyType.Next:
					Control.ReturnKeyType = UIReturnKeyType.Next;
					break;
				case ReturnKeyType.Send:
					Control.ReturnKeyType = UIReturnKeyType.Send;
					break;
				case ReturnKeyType.Search:
					Control.ReturnKeyType = UIReturnKeyType.Search;
					break;
				case ReturnKeyType.Done:
					Control.ReturnKeyType = UIReturnKeyType.Done;
					break;
				default:
					Control.ReturnKeyType = UIReturnKeyType.Default;
					break;
			}
		}
	}
}
