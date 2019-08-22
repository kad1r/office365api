using ExchangeTutorial.Helpers;
using ExchangeTutorial.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ExchangeTutorial.Controllers
{
	public class AuthController : Controller
	{
		public ActionResult Index(string code)
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

			if (!string.IsNullOrWhiteSpace(code))
			{
				var parameters = RestSharpHelper.GenerateAuthenticationParameters(code, vm.RedirectUri, vm.ClientId, vm.ClientSecret);
				var headers = new Dictionary<string, string>();

				headers.Add("Accept", "application/json");
				headers.Add("Content-Type", "application/x-www-form-urlencoded");

				var response = RestSharpHelper.Request(vm.TokenUri, Method.POST, DataFormat.Json, headers, parameters);

				if (response.IsSuccessful)
				{
					var status = response.StatusCode;
					var content = response.Content;

					if (status == System.Net.HttpStatusCode.OK)
					{
						var authToken = JsonConvert.DeserializeObject<AuthToken>(content);
						TempData["Auth"] = authToken;

						return RedirectToAction("List", "Home");
					}
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}

			return View();
		}
	}
}
