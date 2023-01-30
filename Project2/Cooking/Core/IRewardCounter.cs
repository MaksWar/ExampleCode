using Game.Cooking.Meals.Snacks;

namespace Game.Cooking.Core
{
	public interface IRewardCounter
	{
		public int FinalAward { get; }

		void InitReward(int minReward, int rewardForOneRightMeal);

		int DoReward((Snack.MealType, Snack.MealType) meals, float spentTimeOnRequest);
	}
}