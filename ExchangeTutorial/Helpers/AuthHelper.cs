using System.Configuration;

namespace ExchangeTutorial.Helpers
{
	public static class AuthHelper
	{
		public static string GetConfig(string key)
		{
			return ConfigurationManager.AppSettings[key] ?? "";
		}
	}
}
