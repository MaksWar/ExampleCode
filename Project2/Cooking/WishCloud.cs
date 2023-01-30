using System;
using Additions.RetractableUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Cooking
{
	public class WishCloud : MonoBehaviour
	{
		[SerializeField] private RetractablePanel retractablePanel;

		[SerializeField] private Image firstMeal;
		[SerializeField] private Image secondMeal;
		[SerializeField] private Image plus;

		public void Show(Sprite sprite, Action onComplete = null)
		{
			retractablePanel.Show(scale: 1, completed: delegate
			{
				firstMeal.sprite = sprite;

				ActivateOneMeal();

				onComplete?.Invoke();
			});
		}

		public void Show((Sprite, Sprite) sprites, Action onComplete = null)
		{
			retractablePanel.Show(scale: 1, completed: delegate
			{
				firstMeal.sprite = sprites.Item1;
				secondMeal.sprite = sprites.Item2;

				ActivateTwoMeal();

				onComplete?.Invoke();
			});
		}

		public void Hide(float duration = 0.8f, Action onCompete = null)
		{
			retractablePanel.Hide(time: duration, scale: 0, completed: delegate()
			{
				DeactivateAll();

				onCompete?.Invoke();
			});
		}

		private void ActivateTwoMeal()
		{
			firstMeal.gameObject.SetActive(true);
			secondMeal.gameObject.SetActive(true);
			plus.gameObject.SetActive(true);
		}

		private void DeactivateAll()
		{
			firstMeal.gameObject.SetActive(false);
			secondMeal.gameObject.SetActive(false);
			plus.gameObject.SetActive(false);
		}

		private void ActivateOneMeal() =>
			firstMeal.gameObject.SetActive(true);
	}
}