using Game.Cooking.Meals.Data;
using Game.Cooking.Meals.Snacks;

namespace Game.Cooking.Core
{
	public interface IClientWishes
	{
		(MealData, MealData) Wishes { get; }

		(Snack.MealType, Snack.MealType) WishesType { get; }

		int CountOfWishes { get; }

		float DesireTime { get; }

		void InitClientWishes(float desireTime);
	}
}