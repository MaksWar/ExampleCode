using Game.Cooking.Meals.Data;
using Game.Cooking.Meals.Snacks;
using UnityEngine;

namespace Game.Cooking.Core
{
	public class ClientWishes : IClientWishes
	{
		private float _desireTime;

		private readonly IMealsDataContainer _mealsDataContainer;

		private (MealData, MealData) _wishes;

		public (MealData, MealData) Wishes => _wishes;

		public float DesireTime => _desireTime;

		public (Snack.MealType, Snack.MealType) WishesType
		{
			get
			{
				(Snack.MealType, Snack.MealType) wishes;

				wishes.Item1 = _wishes.Item1 != null ? _wishes.Item1.MealType : Snack.MealType.None;
				wishes.Item2 = _wishes.Item2 != null ? _wishes.Item2.MealType : Snack.MealType.None;

				return wishes;
			}
		}

		public int CountOfWishes
		{
			get
			{
				int count = 0;

				if (_wishes.Item1 != null)
					count++;
				if (_wishes.Item2 != null)
					count++;

				return count;
			}
		}

		public ClientWishes(IMealsDataContainer mealsDataContainer) =>
			_mealsDataContainer = mealsDataContainer;

		public void InitClientWishes(float desireTime)
		{
			ClearWishes();

			_desireTime = desireTime;
			if (Random.Range(0, 2) == 0)
				_wishes.Item1 = _mealsDataContainer.GetRandomData();
			else
				_wishes = _mealsDataContainer.GetRandomTupleData();
		}

		private void ClearWishes() =>
			_wishes = (null, null);
	}
}