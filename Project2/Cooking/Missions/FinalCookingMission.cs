using Additions.Missions;
using Duckject.Core.Attributes;
using Game.Cooking.Core;
using UnityEngine;

namespace Game.Cooking.Missions
{
	public class FinalCookingMission : MissionBase
	{
		private IRewardCounter _rewardCounter;
		private IInputController _inputController;

		[Quack]
		private void Construct(IRewardCounter rewardCounter, IInputController inputController)
		{
			_inputController = inputController;
			_rewardCounter = rewardCounter;
		}

		protected override void Mission()
		{
			_inputController.LockAllInput();
			Debug.Log(_rewardCounter.FinalAward);
		}
	}
}