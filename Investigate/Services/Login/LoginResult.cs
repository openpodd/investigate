namespace Investigate
{
	public class LoginResult
	{
		public string UserName
		{
			get;
		}

		public bool Success
		{
			get;
		}

		public string Message
		{
			get;
		}

		public LoginResult(string userName, bool success, string message)
		{
			this.UserName = userName;
			this.Success = success;
			this.Message = message;
		}
	}
}
