using System;

namespace ExchangeTutorial.Helpers
{
	public static class DateHelper
	{
		public static string GetDaysAgo(DateTime date)
		{
			var result = string.Empty;
			var differenceInHours = (DateTime.Now - date).TotalHours;
			var differenceInDays = (DateTime.Now - date).TotalDays;

			if (differenceInDays > 0)
			{
				if (differenceInDays > 365)
				{
					// getting average
					result = (int)differenceInDays / 365 + " years ago";
				}
				else if (differenceInDays <= 365)
				{
					// getting average
					result = (int)differenceInDays / 30 + " months ago";
				}
				else
				{
					result = differenceInDays + " days ago";
				}
			}
			else
			{
				result = differenceInHours + " hours ago";
			}

			return result;
		}
	}
}
