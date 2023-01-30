using System;
using Game.Cooking.Meals;
using Game.Cooking.Meals.Burgers;
using Game.Cooking.Meals.Data;

namespace Game.Cooking.Core
{
	public class BurgerConstructor : IBurgerConstructor
	{
		private Burger _currentBurger;

		private readonly ICookingInputController _inputController;

		public bool IsConstructing => _currentBurger != null;

		public event Action OnStartConstruct;
		public event Action<MealBase> OnEndConstruct;

		public BurgerConstructor(ICookingInputController inputController) =>
			_inputController = inputController;

		public void InitBurger(Burger burger)
		{
			_currentBurger = burger;

			_currentBurger.OnPartAdded += ControlInput;
			_currentBurger.OnLastPartAdded += BurgerCompleted;
		}

		private void BurgerCompleted() =>
			OnEndConstruct?.Invoke(_currentBurger);

		public bool TryConstruct(PartOfBurger part)
		{
			if (_currentBurger == null)
				return false;

			return _currentBurger.TryAddLayer(part);
		}

		public void Construct(PartOfBurger part)
		{
			if (_currentBurger == null) return;

			_currentBurger.AddLayer(part);
		}

		private void ControlInput(TypeOfPart type) =>
			_inputController.LockPartHolder(type);
	}
}