using ExchangeTutorial.Helpers;
using ExchangeTutorial.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ExchangeTutorial.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var vm = new AuthVM
			{
				AuthUri = AuthHelper.GetConfig("AuthUri"),
				TokenUri = AuthHelper.GetConfig("TokenUri"),
				ClientId = AuthHelper.GetConfig("ClientId"),
				ClientSecret = AuthHelper.GetConfig("ClientSecret"),
				RedirectUri = AuthHelper.GetConfig("RedirectUri"),
				Scopes = AuthHelper.GetConfig("Scopes")
			};

			return View(vm);
		}

		/// <summary>
		/// List page to show emails
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public ActionResult List()
		{
			var vm = new MailItem();

			if (TempData["Auth"] != null)
			{
				var authToken = (AuthToken)TempData["Auth"];
				var mailApiUri = AuthHelper.GetConfig("MailBoxApi");

				if (!string.IsNullOrWhiteSpace(mailApiUri) && authToken != null && !string.IsNullOrWhiteSpace(authToken.AccessToken))
				{
					var parameters = RestSharpHelper.GenerateAuthorizationParameters();
					var headers = new Dictionary<string, string>();

					headers.Add("Accept", "application/json");
					headers.Add("Authorization", "Bearer " + authToken.AccessToken);

					var response = RestSharpHelper.Request(mailApiUri, Method.GET, DataFormat.Json, headers, parameters);

					if (response.IsSuccessful)
					{
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
						{
							vm = JsonConvert.DeserializeObject<MailItem>(response.Content);
						}
					}
				}

				TempData["Auth"] = authToken;
			}

			return View(vm);
		}
	}
}
