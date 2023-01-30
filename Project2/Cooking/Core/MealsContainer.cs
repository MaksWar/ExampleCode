using System.Collections.Generic;
using System.Linq;
using Game.Cooking.Meals.Snacks;

namespace Game.Cooking.Core
{
	public class MealsContainer : IMealsContainer
	{
		private readonly List<Snack> _meals;

		public MealsContainer(List<Snack> meals) =>
			_meals = meals;

		public IReadOnlyList<Snack> Meals => _meals;

		public Snack GetMeal(Snack.MealType type) =>
			_meals.FirstOrDefault(x => x.Type == type);
	}
}