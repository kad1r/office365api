using Newtonsoft.Json;
using System;

namespace ExchangeTutorial.Models
{
	public class AuthToken
	{
		private DateTime _createdDate;

		public AuthToken()
		{
			_createdDate = DateTime.Now;
		}

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty("expires_on")]
		public long ExpiresOn { get; set; }

		[JsonProperty("resource")]
		public string Resource { get; set; }

		public string Token => $"{TokenType} {AccessToken}";
		public bool IsExpired => DateTime.Now > ExpirationDate;
		private DateTime ExpirationDate => CreateExpirationDate();

		private DateTime CreateExpirationDate()
		{
			var expireDate = DateTime.MinValue;

			if (ExpiresOn > 0)
			{
				expireDate = new DateTime(ExpiresOn);
			}
			else if (ExpiresIn > 0)
			{
				expireDate = _createdDate.AddSeconds(ExpiresIn);
			}

			return expireDate;
		}
	}
}
