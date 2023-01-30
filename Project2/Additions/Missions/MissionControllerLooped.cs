using UnityEngine;

namespace Additions.Missions
{
	public class MissionControllerLooped : MissionController
	{
		[Header("Индекс входа в цикл")]
		[SerializeField] private int loopEnterIndex;
		[Header("Индекс выхода из цикла")]
		[SerializeField] private int loopExitIndex;
		[Header("Кол-во циклов")]
		[SerializeField] private int loopsCount;

		private int _completedLoopsCount;

		public int LoopsCount => loopsCount;

		public int CompletedLoopsCount => _completedLoopsCount;

		protected override void OnMissionEnd(MissionBase obj)
		{
			CallOnMissionEnded();

			if (Index < missionSequence.Count - 1 || NeedLoop())
				NextMission();
			else
				LoadNextScene();
		}

		protected override void NextMission()
		{
			if (NeedEnterLoop(Index))
				_completedLoopsCount++;

			if (NeedRestartLoop(Index))
			{
				Index = loopEnterIndex;
				missionSequence[Index].EnableMission();

				return;
			}

			missionSequence[++Index].EnableMission();
		}

		private bool NeedEnterLoop(int currentIndex) =>
			NeedLoop() && currentIndex == loopEnterIndex;


		private bool NeedRestartLoop(int currentIndex) =>
			NeedLoop() && currentIndex == loopExitIndex;

		private bool NeedLoop() =>
			_completedLoopsCount < loopsCount;
	}
}