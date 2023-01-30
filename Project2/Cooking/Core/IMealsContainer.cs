using System.Collections.Generic;
using Game.Cooking.Meals.Snacks;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;

namespace Game.Cooking.Core
{
	public interface IMealsContainer
	{
		IReadOnlyList<Snack> Meals { get; }

		Snack GetMeal(MealType type);
	}
}