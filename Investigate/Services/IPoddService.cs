using System;

using System.Threading.Tasks;

namespace Investigate
{
	public interface IPoddService
	{
		Task<SearchResult> Search(SearchRequest searchRequest);

		Task<LoginResult> Login(LoginRequest request);

	    Task<AuthorityResult> GetAuthorities();
	}
}
