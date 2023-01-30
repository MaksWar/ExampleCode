using UnityEngine;
using MealType = Game.Cooking.Meals.Snacks.Snack.MealType;

namespace Game.Cooking.Meals
{
	public class MealBase : MonoBehaviour
	{
		[Header("Type of Meal")]
		[SerializeField] protected MealType type;

		protected MealPlace CurrentPlace;

		public MealType Type => type;
	}
}