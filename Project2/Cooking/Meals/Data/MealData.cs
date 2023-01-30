using UnityEngine;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;

namespace Game.Cooking.Meals.Data
{
	[CreateAssetMenu(menuName = "Cooking/Create MealData", fileName = "MealData", order = 0)]
	public class MealData : ScriptableObject
	{
		[SerializeField] private Sprite mealSprite;
		[SerializeField] private MealType mealType;

		public Sprite MealSprite => mealSprite;

		public MealType MealType => mealType;
	}
}