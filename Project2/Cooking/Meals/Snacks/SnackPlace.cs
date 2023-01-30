namespace Game.Cooking.Meals.Snacks
{
	public class SnackPlace : MealPlace
	{
		private Snack _currentSnack;

		public override MealBase CurrentMeal => _currentSnack;

		public override void TakeAPlace(MealBase meal)
		{
			if (_currentSnack != null && _currentSnack != meal)
				_currentSnack.DragToStartPosition();

			_currentSnack = (Snack) meal;

			OnMealPlaced?.Invoke();
		}

		public override void RemoveEqualMeal(MealBase meal)
		{
			if(_currentSnack != meal) return;

			_currentSnack = null;

			OnMealRemoved?.Invoke();
		}
	}
}