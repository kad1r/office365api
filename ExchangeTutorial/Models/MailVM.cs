using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeTutorial.Models
{
	public class MailVM
	{
		public EmailAddress From { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime DateSended { get; set; }
		public bool HasAttachment { get; set; }
		public bool IsNew { get; set; }
	}
}
