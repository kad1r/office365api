using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExchangeTutorial.Models
{
	public class MailItem
	{
		[JsonProperty("@odata.context")]
		public string Context { get; set; }

		[JsonProperty("value")]
		public List<Value> Values { get; set; }
	}

	public class Value
	{
		[JsonProperty("@odata.etag")]
		public string Etag { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("receivedDateTime")]
		public DateTime ReceivedDateTime { get; set; }

		[JsonProperty("subject")]
		public string Subject { get; set; }

		[JsonProperty("body")]
		public Body Body { get; set; }

		[JsonProperty("from")]
		public From From { get; set; }
	}

	public class Body
	{
		[JsonProperty("contentType")]
		public string ContentType { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }
	}

	public class From
	{
		[JsonProperty("emailAddress")]
		public Email EmailAddress { get; set; }
	}

	public class Email
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }
	}
}
