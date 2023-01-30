using UnityEngine;

namespace Additions.Extensions
{
	public static class CameraExtension
	{
		public static float GetHeight(this Camera camera) => 2f * camera.orthographicSize;

		public static float GetWidth(this Camera camera) => camera.GetHeight() * camera.aspect; 
	}
}