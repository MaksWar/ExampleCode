using Infrastructure;
using Zenject;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;

		public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter() =>
			_sceneLoader.Load(Scenes.InitialScene, onLoaded: EnterLoadLevel);

		public void Exit()
		{
		}

		private void EnterLoadLevel() =>
			_gameStateMachine.Enter<LoadProgressState>();

		public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
		{
		}
	}
}