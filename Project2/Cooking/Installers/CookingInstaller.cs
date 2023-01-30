using System.Collections.Generic;
using Duckject.Core.Container;
using Duckject.Core.Installer;
using Game.Cooking.Core;
using Game.Cooking.Meals.Burgers;
using Game.Cooking.Meals.Data;
using Game.Cooking.Meals.Snacks;
using UnityEngine;

namespace Game.Cooking.Installers
{
	public class CookingInstaller : InstallerBase, ICoroutineRunner
	{
		[Header("Meals Data")]
		[SerializeField] private List<MealData> mealsData;
		[SerializeField] private List<Snack> meals;

		[Header("Burger Data")]
		[SerializeField] private List<BurgerPartHolder> partHolders;

		[Header("Scene Components"), Space(15)]
		[SerializeField] private WishCloud wishCloud;

		private IMealsDataContainer _mealsDataContainer;
		private IMealsContainer _mealsContainer;
		private ICookingInputController _inputController;
		private IClientWishes _clientWishes;
		private IRewardCounter _rewardCounter;
		private IBurgerConstructor _burgerConstructor;
		private ITimeCounter _timeCounter;

		public override void Install(DIContainer container)
		{
			_timeCounter = new TimeCounter(this);
			_mealsDataContainer = new MealsDataContainer(mealsData);
			_mealsContainer = new MealsContainer(meals);
			_inputController = new CookingInputController(meals, partHolders);
			_clientWishes = new ClientWishes(_mealsDataContainer);
			_rewardCounter = new RewardCounter(_clientWishes);
			_burgerConstructor = new BurgerConstructor(_inputController);

			container.Bind(_timeCounter);
			container.Bind(_mealsDataContainer);
			container.Bind(_mealsContainer);
			container.Bind(_inputController);
			container.Bind(_clientWishes);
			container.Bind(_rewardCounter);
			container.Bind(_burgerConstructor);
			container.Bind(wishCloud);
		}
	}
}