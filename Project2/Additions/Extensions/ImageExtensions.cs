using UnityEngine;
using UnityEngine.UI;

namespace Additions.Extensions
{
	public static class ImageExtensions
	{
		public static void FitRectTransform(this Image image, RectTransform rectTransform)
		{
			image.SetNativeSize();
			var maxFrameLength = rectTransform.sizeDelta.GetMaxValue();
			float multiplier = maxFrameLength / image.rectTransform.sizeDelta.GetMaxValue();
			image.rectTransform.sizeDelta *= multiplier;
		}
	}
}