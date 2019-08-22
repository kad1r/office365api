using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeTutorial.Models
{
	public class AuthVM
	{
		public string AuthUri { get; set; }
		public string TokenUri { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string RedirectUri { get; set; }
		public string Scopes { get; set; }
	}
}