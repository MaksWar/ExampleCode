namespace Additions.Extensions
{
	public static class TimeExtensions
	{
		public static string SecondsToMinutesSecondsTimeStringFormat(this float time)
		{
			float seconds = time % 60;
			float minutes = (time - time % 60) / 60;

			var timeText = $"{minutes:00}:{seconds:00}";

			return timeText;
		}

		public static string SecondsToHoursMinTimeStringFormat(this float time)
		{
			float minutes = (time - time % 60) / 60;
			float hours = (minutes - minutes % 60) / 60;

			minutes %= 60;

			var timeText = $"{hours:00}:{minutes:00}";

			return timeText;
		}

		public static string SecondsToFullTimeStringFormat(this float time)
		{
			float seconds = time % 60;
			float minutes = (time - time % 60) / 60;
			float hours = (minutes - minutes % 60) / 60;

			minutes %= 60;

			var timeText = $"{hours:00}:{minutes:00}:{seconds:00}";

			return timeText;
		}
	}
}