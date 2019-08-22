using ExchangeTutorial.Helpers;
using ExchangeTutorial.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ExchangeTutorial.Controllers
{
	public class MailController : Controller
	{
		public ActionResult Index()
		{
			var vm = new List<MailVM>();
			var service = new ExchangeService
			{
				Credentials = new WebCredentials(AuthHelper.GetConfig("Email"), AuthHelper.GetConfig("Pwd")),
				Url = new Uri(AuthHelper.GetConfig("ExchangeUri"))
			};

			foreach (var mail in service.FindItems(WellKnownFolderName.Inbox, new ItemView(int.Parse(AuthHelper.GetConfig("PageSize")))))
			{
				mail.Load(new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.TextBody));

				var from = new EmailAddress();
				var mailItem = new MailVM
				{
					Body = mail.Body,
					DateSended = mail.DateTimeSent,
					Subject = mail.Subject,
					HasAttachment = mail.HasAttachments,
					IsNew = mail.IsNew
				};

				foreach (PropertyDefinitionBase prop in mail.GetLoadedPropertyDefinitions())
				{
					if (prop.Type.Name == "EmailAddress")
					{
						mail.TryGetProperty<EmailAddress>(prop, out from);
						mailItem.From = from;
						break;
					}
				}

				vm.Add(mailItem);
			}

			return View(vm);
		}
	}
}
