using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using Investigate.iOS;
using Investigate;

[assembly: ExportRenderer(typeof(UserNameEntry), typeof(NextEntryRenderer))]
namespace Investigate.iOS
{
	public class NextEntryRenderer : NoCapEntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
		}
	}
}
