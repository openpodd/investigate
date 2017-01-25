using System;


namespace Investigate
{


	public partial class PoddService : IPoddService
	{

		String ServerUrl
		{
			get;
			set;
		}


		public PoddService()
		{
			ServerUrl = "https://api.cmonehealth.org";
			ServerUrl = "http://10.0.3.37:32770";
		}
			

	}
}
