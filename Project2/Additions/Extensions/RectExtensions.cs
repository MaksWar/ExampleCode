using UnityEngine;

namespace Additions.Extensions
{
	public static class RectExtensions
	{
		public static Vector2 GetRandomPosition(this Rect rect)
		{
			var randX = Random.Range(rect.xMin, rect.xMax);
			var randY = Random.Range(rect.yMin, rect.yMax);
			return new Vector2(randX, randY);
		}
	}
}