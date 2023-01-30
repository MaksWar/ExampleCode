using System;
using System.Collections.Generic;
using System.Linq;
using Duckject.Core.Attributes;
using Game.Cooking.Core;
using Game.Cooking.Meals;
using UnityEngine;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;

namespace Game.Cooking
{
	public class Tray : MonoBehaviour
	{
		[SerializeField] private List<MealPlace> positionOfSnack;

		private IMealsDataContainer _container;

		private int _targetCount = 2;

		public event Action OnMealsAvailable;
		public event Action OnMealsUnAvailable;

		public (MealType, MealType) CurrentMeals
		{
			get
			{
				var mealTypes = MealsOnPositions();
				if (mealTypes.Count == 0)
					return (MealType.None, MealType.None);

				var first = mealTypes.First();
				var last = mealTypes.Last();

				return (first, last == first ? MealType.None : last);
			}
		}

		[Quack]
		private void Construct(IMealsDataContainer container) =>
			_container = container;

		private void OnEnable()
		{
			positionOfSnack.ForEach(x =>
			{
				x.OnMealPlaced += CheckAvailability;
				x.OnMealRemoved += MealRemoved;
			});
		}

		private void OnDisable()
		{
			positionOfSnack.ForEach(x =>
			{
				x.OnMealPlaced -= CheckAvailability;
				x.OnMealRemoved -= MealRemoved;
			});
		}

		public MealPlace GetPosition(MealType mealType)
		{
			var subGroup = _container.GetMealSubGroup(mealType);

			return positionOfSnack.FirstOrDefault(x => x.SubGroup == subGroup);
		}

		public void SetTargetCountOfMeals(int count) =>
			_targetCount = count;

		private void CheckAvailability()
		{
			if (MealsOnPositions().Count == _targetCount)
				OnMealsAvailable?.Invoke();
		}

		private void MealRemoved()
		{
			if (MealsOnPositions().Count < _targetCount)
				OnMealsUnAvailable?.Invoke();
		}

		private List<MealType> MealsOnPositions()
		{
			return (from meal in positionOfSnack.Select(x => x.CurrentMeal)
				where meal != null
				select meal.Type).ToList();
		}
	}
}