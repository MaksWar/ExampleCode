using UnityEngine;

namespace Game.Cooking.Meals.Burgers
{
	public class BurgerPlace : MealPlace
	{
		private Burger _currentBurger;

		public override MealBase CurrentMeal => _currentBurger;

		public override void TakeAPlace(MealBase meal)
		{
			if(_currentBurger != null && _currentBurger != meal)
				Debug.Log("Revert");

			_currentBurger = (Burger) meal;

			OnMealPlaced?.Invoke();
		}

		public override void RemoveEqualMeal(MealBase meal)
		{
			if(_currentBurger != meal) return;

			_currentBurger = null;

			OnMealRemoved?.Invoke();
		}
	}
}