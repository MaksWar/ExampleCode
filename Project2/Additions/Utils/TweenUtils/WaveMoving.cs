using Additions.Extensions;
using DG.Tweening;
using UnityEngine;

namespace Additions.Utils.TweenUtils
{
	public class WaveMoving : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float amplitude;
		[SerializeField] private Ease ease;
		[SerializeField] private float delay;

		private void Start()
		{
			this.DelayedCall(delay, StartWaviness);
		}

		private void StartWaviness()
		{
			var endValue = transform.localPosition.y + amplitude;
			transform.DOLocalMoveY(endValue, speed)
				.SetEase(ease)
				.SetLoops(-1, LoopType.Yoyo)
				;
		}
	}
}