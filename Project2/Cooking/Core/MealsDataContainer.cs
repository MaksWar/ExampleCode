using System;
using System.Collections.Generic;
using System.Linq;
using Game.Cooking.Meals.Data;
using PSV;
using UnityEngine;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;
using MealSubGroups = Game.Cooking.Meals.Snacks.Snack.MealSubGroups;

namespace Game.Cooking.Core
{
	public class MealsDataContainer : IMealsDataContainer
	{
		private readonly List<MealData> _mealsData;

		private readonly Dictionary<MealSubGroups, List<MealType>> _groupedMeals =
			new Dictionary<MealSubGroups, List<MealType>>()
			{
				[MealSubGroups.Drink] = new List<MealType>()
				{
					MealType.Tea,
					MealType.Coffee,
				},
				[MealSubGroups.Snack] = new List<MealType>()
				{
					MealType.Candy,
					MealType.Cookie,
				},
				[MealSubGroups.Burger] = new List<MealType>()
				{
					MealType.Burger,
				},
			};

		public IReadOnlyList<MealData> MealsData => _mealsData;

		public MealsDataContainer(List<MealData> mealsData) =>
			_mealsData = mealsData;

		public MealSubGroups GetMealSubGroup(MealType mealType)
		{
			foreach (var group in _groupedMeals)
			{
				if (group.Value.Contains(mealType))
					return group.Key;
			}

			Debug.Log("<color=red>Такого типу немає в саб группах, повернуто першу саб группу </color>");

			return _groupedMeals.First().Key;
		}

		public MealData GetData(MealType mealType) =>
			_mealsData.FirstOrDefault(x => x.MealType == mealType);

		public MealData GetRandomData() =>
			_mealsData.GetRandomElement();

		public (MealData, MealData) GetRandomTupleData()
		{
			var elementsForRandom = _mealsData.Shuffle();

			var firstElement = GetUniqElement(elementsForRandom, GetRandomSubGroup());
			var secondElement = elementsForRandom.Last();

			return (firstElement, secondElement);

			MealData GetUniqElement(List<MealData> mealsData, MealSubGroups mealSubGroup)
			{
				var randomType = _groupedMeals[mealSubGroup].GetRandomElement();
				var element = mealsData.First(x => x.MealType == randomType);

				RemoveElementsThisSubGroups(mealSubGroup);

				return element;
			}

			void RemoveElementsThisSubGroups(MealSubGroups subGroups)
			{
				var mealTypes = _groupedMeals[subGroups].ToList();
				mealTypes.ForEach(x => elementsForRandom.Remove(elementsForRandom.First(y => y.MealType == x)));
			}
		}

		private MealSubGroups GetRandomSubGroup() =>
			Enum.GetValues(typeof(MealSubGroups)).Cast<MealSubGroups>().ToList().GetRandomElement();
	}
}