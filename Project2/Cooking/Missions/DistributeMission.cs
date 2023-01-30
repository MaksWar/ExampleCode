using System.Collections.Generic;
using System.Linq;
using Additions.Extensions;
using Additions.Missions;
using Additions.RetractableUI;
using Duckject.Core.Attributes;
using Game.Cooking.Core;
using Game.Cooking.Meals.Snacks;
using UnityEngine;

namespace Game.Cooking.Missions
{
	public class DistributeMission : MissionBase
	{
		[SerializeField] private Tray tray;
		[SerializeField] private Plate plate;
		[SerializeField] private RetractableButton button;
		[SerializeField] private RetractableScrollBar progressBar;

		[Header("Нагорода"), Space(15)]
		[SerializeField, Tooltip("За одне правильне блюдо")] private int reward;
		[SerializeField] private int minReward;

		private IInputController _inputController;
		private IMealsContainer _mealsContainer;
		private IRewardCounter _rewardCounter;
		private IClientWishes _clientWishes;
		private ITimeCounter _timeCounter;
		private WishCloud _wishCloud;

		private List<Snack> _meals;

		private void OnEnable()
		{
			button.OnClick += FinishDistribute;

			tray.OnMealsAvailable += ReadyToGetReward;
			tray.OnMealsUnAvailable += UnreadyToGetReward;

			_meals.ForEach(x => x.OnMealSelected += InitTargetPlace);
		}

		private void OnDisable()
		{
			button.OnClick -= FinishDistribute;

			tray.OnMealsAvailable -= ReadyToGetReward;
			tray.OnMealsUnAvailable -= UnreadyToGetReward;

			_meals.ForEach(x => x.OnMealSelected -= InitTargetPlace);
		}

		private void OnDestroy() =>
			_timeCounter.OnTimeChanged -= TimeView;

		[Quack]
		private void Construct(IMealsContainer mealsContainer, IInputController inputController
			, IRewardCounter rewardCounter, IClientWishes clientWishes, ITimeCounter timeCounter,
			WishCloud wishCloud)
		{
			_timeCounter = timeCounter;
			_clientWishes = clientWishes;
			_rewardCounter = rewardCounter;
			_inputController = inputController;
			_mealsContainer = mealsContainer;
			_wishCloud = wishCloud;

			InitMeals();
		}

		protected override void Mission()
		{
			_timeCounter.Start();
			_timeCounter.OnTimeChanged += TimeView;

			tray.SetTargetCountOfMeals(_clientWishes.CountOfWishes);
			_rewardCounter.InitReward(minReward, reward);

			_inputController.UnlockAllInput();
		}

		private void ReadyToGetReward() =>
			button.Show();

		private void UnreadyToGetReward() =>
			button.Hide();

		private void FinishDistribute()
		{
			_timeCounter.Stop();

			_meals.ForEach(x => x.DragToStartPosition());
			plate.ResetBurger();

			button.Hide();
			_wishCloud.Hide(onCompete: End);

			_rewardCounter.DoReward(_clientWishes.WishesType, _timeCounter.Seconds);

			//TODO Реализовать уход персонажа
		}

		private void TimeView()
		{
			progressBar.SetProgress(
				Mathf.Clamp(GetTimePercentage().ConvertPercentTo01(), 0f, 1f), 0.1f);

			float GetTimePercentage() =>
				100 - _timeCounter.Seconds.GetPercentFromValue(_clientWishes.DesireTime);
		}

		private void InitTargetPlace(Snack snack) =>
			snack.SetDropTarget(tray.GetPosition(snack.Type));

		private void InitMeals() =>
			_meals = _mealsContainer.Meals.ToList();
	}
}