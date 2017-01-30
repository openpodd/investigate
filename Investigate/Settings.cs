using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Investigate
{
	public static class Settings
	{
		private static ISettings AppSettings => CrossSettings.Current;



	    private const string TokenKey = "token";

		public static string Token
		{
			get
			{
				return AppSettings.GetValueOrDefault(TokenKey, "");
			}
			set
			{
				AppSettings.AddOrUpdateValue(TokenKey, value);
			}
		}

		public static bool IsLogin() 
		{
			return Token != "";
		}



	    private const string AuthoritiesKey = "authorities";

	    public static string Authorities
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(AuthoritiesKey, "");
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(AuthoritiesKey, value);
	        }
	    }


	    public static void Clear()
	    {
	        Authorities = "";
	        Token = "";
	    }
	}
}
