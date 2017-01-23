using System;
namespace Investigate
{
	public class LoginResult
	{
		public String UserName
		{
			get;
		}

		public bool Success
		{
			get;
		}

		public String Message
		{
			get;
		}

		public LoginResult(String userName, bool success, String message)
		{
			this.UserName = userName;
			this.Success = success;
			this.Message = message;
		}
	}
}
