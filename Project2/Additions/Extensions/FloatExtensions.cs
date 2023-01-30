namespace Additions.Extensions
{
	public static class FloatExtensions
	{
		public static float GetPercentFromValue(this float currentValue, float maxValue) =>
			(100 * currentValue) / maxValue;

		public static float GetValueFromPercent(this float currentPercent, float maxValue) =>
			(maxValue * currentPercent) / 100;

		public static float ConvertPercentTo01(this float value) =>
			value * 0.01f;
	}
}