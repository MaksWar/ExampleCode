using Game.Cooking.Meals.Snacks;
using UnityEngine;

namespace Game.Cooking.Core
{
	public class RewardCounter : IRewardCounter
	{
		private int _finalAward;
		private int _minRewardValue;
		private int _rewardForOneRightMeal;

		private readonly IClientWishes _clientWishes;

		private const float maxCoefficient = 1f;

		public int FinalAward => _finalAward;

		public RewardCounter(IClientWishes clientWishes) =>
			_clientWishes = clientWishes;

		public void InitReward(int minReward, int rewardForOneRightMeal)
		{
			if (minReward < 0 || rewardForOneRightMeal < 0) return;

			_rewardForOneRightMeal = rewardForOneRightMeal;
			_minRewardValue = minReward;
		}

		public int DoReward((Snack.MealType, Snack.MealType) meals, float spentTimeOnRequest)
		{
			int reward = 0;

			if (ComparePreparedMealToWishes(meals.Item1))
				reward += _rewardForOneRightMeal;
			if (ComparePreparedMealToWishes(meals.Item2))
				reward += _rewardForOneRightMeal;

			reward = (int) (reward * GetTimeCoefficient(spentTimeOnRequest, _clientWishes.DesireTime));
			reward = reward > _minRewardValue ? reward : _minRewardValue;

			_finalAward += reward;

			return reward;
		}

		private float GetTimeCoefficient(float spentTime, float requestTime)
		{
			float expiredTimeCoefficient = spentTime / requestTime - maxCoefficient;

			return maxCoefficient - Mathf.Clamp01(expiredTimeCoefficient);
		}

		private bool ComparePreparedMealToWishes(Snack.MealType type) =>
			(type == _clientWishes.WishesType.Item1 || type == _clientWishes.WishesType.Item2) &&
			type != Snack.MealType.None;
	}
}