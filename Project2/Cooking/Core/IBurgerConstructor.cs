using System;
using Game.Cooking.Meals;
using Game.Cooking.Meals.Burgers;

namespace Game.Cooking.Core
{
	public interface IBurgerConstructor
	{
		public event Action OnStartConstruct;
		public event Action<MealBase> OnEndConstruct;

		public bool IsConstructing { get; }

		void InitBurger(Burger burger);

		bool TryConstruct(PartOfBurger part);

		void Construct(PartOfBurger part);
	}
}