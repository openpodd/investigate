namespace Investigate
{
	public class LoginRequest
	{
		public string Username { get; }
		public string Password { get; }

		public LoginRequest(string userName, string password)
		{
			Username = userName;
			Password = password;
		}
	}
}
