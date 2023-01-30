using System;
using Additions.DragNDrop;
using Game.Cooking.Core;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Cooking.Meals.Snacks
{
	[RequireComponent(typeof(SortingGroup))]
	[RequireComponent(typeof(DragNDrop2D))]
	public class Snack : MealBase, IInputControl
	{
		private SortingGroup _sortingGroup;
		private DragNDrop2D _dragAndDrop;

		private int _startSortingOrder;

		private const int dragSortingOrder = 100;

		public event Action<Snack> OnMealSelected;

		private void OnEnable()
		{
			_dragAndDrop.OnBeginDragCustom += MealStartDrag;
			_dragAndDrop.OnEndDragCustom += MealEndDrag;
			_dragAndDrop.OnMovementToTargetCompleted += MealOnTarget;
		}

		private void OnDisable()
		{
			_dragAndDrop.OnBeginDragCustom -= MealStartDrag;
			_dragAndDrop.OnEndDragCustom -= MealEndDrag;
			_dragAndDrop.OnMovementToTargetCompleted -= MealOnTarget;
		}

		private void Awake() =>
			InitSortingOrder();

		public void SetDropTarget(MealPlace mealPlace)
		{
			CurrentPlace = mealPlace;

			_dragAndDrop.Target = mealPlace.gameObject;
		}

		public void DragToStartPosition()
		{
			CurrentPlace?.RemoveEqualMeal(this);

			_dragAndDrop.MoveToDefault();
		}

		public void Lock() =>
			_dragAndDrop.Interactable(false);

		public void UnLock() =>
			_dragAndDrop.Interactable(true);

		private void MealStartDrag(DragNDrop2D obj)
		{
			_sortingGroup.sortingOrder = dragSortingOrder;
			LeaveCurrentPlace();

			OnMealSelected?.Invoke(this);
		}

		private void LeaveCurrentPlace()
		{
			if(CurrentPlace == null) return;

			CurrentPlace.RemoveEqualMeal(this);
			CurrentPlace = null;
		}

		private void MealOnTarget(DragNDrop2D dragNDrop2D)
		{
			ToDefaultSortingOrder();
			CurrentPlace.TakeAPlace(this);
		}

		private void MealEndDrag(DragNDrop2D obj) =>
			ToDefaultSortingOrder();

		private void ToDefaultSortingOrder() =>
			_sortingGroup.sortingOrder = _startSortingOrder;

		private void InitSortingOrder() =>
			_startSortingOrder = _sortingGroup.sortingOrder;

		public enum MealType
		{
			None = 0,
			Cookie = 1,
			Tea = 2,
			Coffee = 3,
			Candy = 4,
			Burger = 5,
		}

		public enum MealSubGroups
		{
			Drink = 1,
			Snack = 2,
			Burger = 3,
		}

		#region Editor

		private void OnValidate()
		{
			_dragAndDrop ??= GetComponent<DragNDrop2D>();
			_sortingGroup ??= GetComponent<SortingGroup>();
		}

		#endregion
	}
}