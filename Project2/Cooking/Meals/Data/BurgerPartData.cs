using UnityEngine;

namespace Game.Cooking.Meals.Data
{
	[CreateAssetMenu(menuName = "Cooking/Create BurgerPartData", fileName = "BurgerPartData", order = 0)]
	public class BurgerPartData : ScriptableObject
	{
		[SerializeField] private TypeOfPart typeOfPart;
		[SerializeField] private Sprite partSprite;

		public TypeOfPart TypeOfPart => typeOfPart;
		public Sprite PartSprite => partSprite;
	}

	public enum TypeOfPart
	{
		DownBun,
		UpperBun,
		Tomato,
		Salad,
		Pepper,
		Cucumber,
	}
}