using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace Investigate
{
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}


		const string TOKEN_KEY = "token";

		public static string Token
		{
			get
			{
				return AppSettings.GetValueOrDefault<String>(TOKEN_KEY, "");
			}
			set
			{
				AppSettings.AddOrUpdateValue<String>(TOKEN_KEY, value);
			}
		}

		public static bool IsLogin() 
		{
			return Token != "";
		}

	}
}
