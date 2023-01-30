using Infrastructure;
using Infrastructure.Services.DataServices.CharacterStats;
using Zenject;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private readonly ICharacterStatsService _characterStatsService;
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader
			, ICharacterStatsService characterStatsService)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_characterStatsService = characterStatsService;
		}

		public void Enter()
		{
			InitCharacterData();

			_sceneLoader.Load(Scenes.InitialScene, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
		}

		private void InitCharacterData() =>
			_characterStatsService.Init();

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<LoadProgressState>();

		public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
		{
		}
	}
}