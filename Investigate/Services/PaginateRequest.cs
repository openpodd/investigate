using System;
using Newtonsoft.Json;

namespace Investigate
{

	public class PaginateRequest
	{
		public int StartPage { set; get;}
		public int ItemPerPage { set; get;}

		public PaginateRequest() : this(1, 20)
		{
		}

		public PaginateRequest(int startPage, int itemPerPage)
		{
			this.StartPage = startPage;
			this.ItemPerPage = itemPerPage;
		}
	}
}
