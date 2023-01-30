using System.Collections.Generic;
using Game.Cooking.Meals.Data;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;
using MealSubGroups = Game.Cooking.Meals.Snacks.Snack.MealSubGroups;

namespace Game.Cooking.Core
{
	public interface IMealsDataContainer
	{
		IReadOnlyList<MealData> MealsData { get; }

		MealSubGroups GetMealSubGroup(MealType mealType);

		MealData GetData(MealType mealType);

		MealData GetRandomData();

		(MealData, MealData) GetRandomTupleData();
	}
}