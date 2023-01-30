using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Additions.RetractableUI
{
	public class RetractableScrollBar : RetractableBase
	{
		[SerializeField] private Image fill;

		public event Action OnFillingBar;

		public void SetProgress(float progressPercentage, float time = 0.5f, Action completed = null) =>
			FillAmount(progressPercentage, time, completed);

		public void AddProgress(float progressPercentage, float time = 0.5f, Action completed = null) =>
			FillAmount(fill.fillAmount + progressPercentage, time, completed);

		private void FillAmount(float progressPercentage, float time, Action completed)
		{
			fill.DOFillAmount(progressPercentage, time).OnComplete(() =>
			{
				if (Math.Abs(fill.fillAmount - 1) == 0)
					OnFillingBar?.Invoke();

				completed();
			});
		}
	}
}