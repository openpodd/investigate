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
		}
			

	}
}
