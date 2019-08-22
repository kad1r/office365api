using RestSharp;
using System;
using System.Collections.Generic;

namespace ExchangeTutorial.Helpers
{
	public static class RestSharpHelper
	{
		public static IRestResponse response;

		public static IRestResponse Request(string uri, Method method, DataFormat format = DataFormat.Json, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
		{
			try
			{
				var client = new RestClient();
				var request = new RestRequest(uri, format);

				foreach (var header in headers)
				{
					request.AddHeader(header.Key, header.Value);
				}

				foreach (var parameter in parameters)
				{
					request.AddParameter(parameter.Key, parameter.Value);
				}

				response = client.Execute(request, method);
			}
			catch (Exception ex)
			{
				// log error
				var msg = ex.Message;
			}

			return response;
		}

		public static Dictionary<string, string> GenerateAuthenticationParameters(string code, string redirect_uri, string client_id, string client_secret)
		{
			var parameters = new Dictionary<string, string>();

			parameters.Add("grant_type", "authorization_code");
			parameters.Add("code", code);
			parameters.Add("redirect_uri", redirect_uri);
			parameters.Add("client_id", client_id);
			parameters.Add("client_secret", client_secret);

			return parameters;
		}

		public static Dictionary<string, string> GenerateAuthorizationParameters()
		{
			var parameters = new Dictionary<string, string>();
			
			parameters.Add("$select", "subject,body,from,attachments,receivedDateTime"); // can be added more odata query parameters from -> https://docs.microsoft.com/en-us/graph/query-parameters
			parameters.Add("$top", AuthHelper.GetConfig("PageSize"));
			parameters.Add("$orderby", "receivedDateTime DESC");

			return parameters;
		}
	}
}
