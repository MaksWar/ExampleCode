using UnityEngine;

namespace Additions.Extensions
{
	public static class ColorExtensions
	{
		public static Color Darken(this Color color, float step)
		{
			color.r -= step;
			color.g -= step;
			color.b -= step;
			return color;
		}
		
		public static Color IncreaseLuminance(this Color color, float step)
		{
			color.r *= step * 0.2126f;
			color.g *= step * 0.7152f;
			color.b *= step * 0.0722f;
			return color;
		}
	}
}