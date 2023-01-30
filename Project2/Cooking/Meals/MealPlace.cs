using System;
using UnityEngine;
using MealSubGroups = Game.Cooking.Meals.Snacks.Snack.MealSubGroups;

namespace Game.Cooking.Meals
{
	public abstract class MealPlace : MonoBehaviour
	{
		[SerializeField] private MealSubGroups subGroup;

		public Action OnMealPlaced;
		public Action OnMealRemoved;

		public MealSubGroups SubGroup => subGroup;

		public abstract MealBase CurrentMeal { get; }

		public abstract void TakeAPlace(MealBase meal);

		public abstract void RemoveEqualMeal(MealBase meal);
	}
}