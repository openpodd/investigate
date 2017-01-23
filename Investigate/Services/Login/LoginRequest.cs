using System;
namespace Investigate
{
	public class LoginRequest
	{
		public String Username { get; }
		public String Password { get; }

		public LoginRequest(String userName, String password)
		{
			this.Username = userName;
			this.Password = password;
		}
	}
}
