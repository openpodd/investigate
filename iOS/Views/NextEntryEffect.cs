using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using Investigate.iOS;
using Investigate;

[assembly:ResolutionGroupName("org.cmonehealth")]
[assembly:ExportEffect(typeof(NextEntryEffect), "NextEntryEffect")]
namespace Investigate.iOS
{
	public class NextEntryEffect : PlatformEffect
	{

		protected override void OnAttached()
		{
			
		}

		protected override void OnDetached()
		{
		}
		
	}
}
