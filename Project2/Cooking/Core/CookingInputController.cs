using System.Collections.Generic;
using System.Linq;
using Game.Cooking.Meals.Burgers;
using Game.Cooking.Meals.Data;
using Game.Cooking.Meals.Snacks;

namespace Game.Cooking.Core
{
	public class CookingInputController : ICookingInputController
	{
		private readonly List<Snack> meals;
		private readonly List<BurgerPartHolder> burgerPartHolders;

		public CookingInputController(List<Snack> meals, List<BurgerPartHolder> partHolders)
		{
			burgerPartHolders = partHolders;
			this.meals = meals;
		}

		public void LockAllInput()
		{
			burgerPartHolders.ForEach(x => x.Lock());
			meals.ForEach(x => x.Lock());
		}

		public void UnlockAllInput()
		{
			burgerPartHolders.ForEach(x =>x.UnLock());
			meals.ForEach(x => x.UnLock());
		}

		public void LockPartHolder(TypeOfPart typeOfPart) =>
			burgerPartHolders.FirstOrDefault(x => x.TypeOfPart == typeOfPart)?.Lock();
	}
}