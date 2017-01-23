using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Text;

using Investigate.Droid;
using Investigate;

[assembly: ExportRenderer(typeof(TextEntry), typeof(TextEntryRenderer))]
namespace Investigate.Droid
{
	public class TextEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			TextEntry entry = (TextEntry)this.Element;

			if (Control != null)
			{
				if (entry != null)
				{
					SetReturnType(entry);

					// Editor Action is called when the return button is pressed
					Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
					{
						if (entry.ReturnKeyType != ReturnKeyType.Next)
						{
							entry.Unfocus();
						}

						// Call all the methods attached to base_entry event handler Completed
						entry.InvokeCompleted();
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
					Control.ImeOptions = ImeAction.Go;
					Control.SetImeActionLabel("Go", ImeAction.Go);
					break;
				case ReturnKeyType.Next:
					Control.ImeOptions = ImeAction.Next;
					Control.SetImeActionLabel("Next", ImeAction.Next);
					break;
				case ReturnKeyType.Send:
					Control.ImeOptions = ImeAction.Send;
					Control.SetImeActionLabel("Send", ImeAction.Send);
					break;
				case ReturnKeyType.Search:
					Control.ImeOptions = ImeAction.Search;
					Control.SetImeActionLabel("Search", ImeAction.Search);
					break;
				default:
					Control.ImeOptions = ImeAction.Done;
					Control.SetImeActionLabel("Done", ImeAction.Done);
					break;
			}
		}
	}
}
