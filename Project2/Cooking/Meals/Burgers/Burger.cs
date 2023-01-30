using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Cooking.Meals.Data;
using Sirenix.Utilities;
using UnityEngine;

namespace Game.Cooking.Meals.Burgers
{
	public class Burger : MealBase
	{
		[Header("Burger Config"), Space(15)]
		[SerializeField] private TypeOfPart firstPart;
		[SerializeField] private TypeOfPart lastPart;

		private Queue<PartOfBurger> _partsQueue;

		private const int maxLayer = 5;

		private Vector3 _posToIngredients;
		private int _currentSortingOrder;

		public event Action<TypeOfPart> OnPartAdded;
		public event Action OnLastPartAdded;

		protected void Awake()
		{
			_partsQueue = new Queue<PartOfBurger>();
			_posToIngredients = transform.position;
		}

		public bool TryAddLayer(PartOfBurger part)
		{
			if (LayerCount() <= 0 && part.Type != firstPart)
				return false;

			if (BurgerReady())
				return false;

			if (_partsQueue.LastOrDefault()?.Type == TypeOfPart.DownBun && part.Type == lastPart)
				return false;

			return true;
		}

		public void AddLayer(PartOfBurger part)
		{
			int layerCount = LayerCount();

			if (layerCount == 0)
				AddFirstLayer(part);
			else if (layerCount < maxLayer)
				AddMiddleLayer(part);
			else
				AddLastLayer(part);
		}

		public void ResetBurger()
		{
			_partsQueue.ForEach(x => Destroy(x.gameObject));
			_partsQueue.Clear();

			_posToIngredients = transform.position;
		}

		private void AddFirstLayer(PartOfBurger part)
		{
			if (part.Type != firstPart) return;

			AddPartToQueue(part);

			_currentSortingOrder = part.SortingOrder;
		}

		private void AddMiddleLayer(PartOfBurger part)
		{
			if (part.Type == firstPart) return;

			if (part.Type == lastPart)
			{
				AddLastLayer(part);

				return;
			}

			AddPartToQueue(part);
		}

		private void AddLastLayer(PartOfBurger part)
		{
			if (part.Type != lastPart) return;

			AddPartToQueue(part);

			OnLastPartAdded?.Invoke();
		}

		private void AddPartToQueue(PartOfBurger part)
		{
			_partsQueue.Enqueue(part);
			OnPartAdded?.Invoke(part.Type);

			part.SetSortingOrder(++_currentSortingOrder);
			part.transform
				.DOMove(_posToIngredients, 1f)
				.SetEase(Ease.OutQuart)
				.OnComplete(part.BounceAnimation);

			_posToIngredients += new Vector3(0, 0.1f, 0);
		}

		private int LayerCount() =>
			_partsQueue.Count;

		private bool BurgerReady() =>
			_partsQueue.LastOrDefault()?.Type == lastPart;
	}
}