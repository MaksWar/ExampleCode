using Game.Data.CharacterStats;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services.DataServices.CharacterStats;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly ICharacterStatsService _characterStatsService;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _savedLoadService;
		private readonly GameStateMachine _stateMachine;

		public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
			ISaveLoadService savedLoadService, ICharacterStatsService characterStatsService)
		{
			_progressService = progressService;
			_savedLoadService = savedLoadService;
			_characterStatsService = characterStatsService;
			_stateMachine = stateMachine;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();
			_stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew() =>
			_progressService.Progress = _savedLoadService.LoadProgress() ?? NewProgress();

		// TODO замінити дебаг перехід
		private PlayerProgress NewProgress()
		{
			var progress = new PlayerProgress(GetNextScene());

			InitCharacterStats(progress);

			return progress;
		}

		private string GetNextScene()
		{
			var debugScene = GameRunner.DebugScene;
			string scene;
			if (debugScene != null)
			{
				scene = debugScene.IsEmpty()
					? Scenes.CharacterTestScene
					: debugScene;
			}
			else
			{
				scene = Scenes.CharacterTestScene;
			}

			return scene;
		}

		private void InitCharacterStats(PlayerProgress progress)
		{
			CharacterStatsData stats = _characterStatsService.GetCharacterStats();

			progress.Stats.MaxHP = stats.MaxHp;
			progress.Stats.MoveSpeed = stats.MoveSpeed;

			progress.Stats.ResetHp();
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, LoadProgressState>
		{
		}
	}
}