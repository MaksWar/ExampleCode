using System;
using DG.Tweening;
using UnityEngine;

namespace Additions.Utils
{
	public class SpriteFader : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		[Header("Configuration"), Space(15)]
		[SerializeField] private float fadeValue;

		public void Fade(float duration = 1f, Action onComplete = null)
		{
			spriteRenderer
				.DOFade(fadeValue, duration)
				.OnComplete(() => onComplete?.Invoke());
		}

		public void UnFade(float duration = 1f, Action onComplete = null)
		{
			spriteRenderer
				.DOFade(0f, duration)
				.OnComplete(() => onComplete?.Invoke());
		}
	}
}