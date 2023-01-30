using UnityEngine;

namespace Additions.Extensions
{
	public static class Vector2Extensions
	{
		public static float GetMaxValue(this Vector2 v2) => v2.x > v2.y ? v2.x : v2.y;

		public static float GetMaxValue(this Vector3 @v3) => Mathf.Max(Mathf.Max(v3.x, v3.y), v3.z);
		
	}
}