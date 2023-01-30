using Game.Scripts.Additions.Camera;
using Infrastructure;
using Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
	public class LoadLevelState : IPayLoadedState<string>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly SceneLoader _sceneLoader;
		private readonly IUiFactory _uiFactory;
		private readonly IPlayerFactory _playerFactory;

		private const string InitialPoint = "InitialPoint";

		public LoadLevelState(GameStateMachine stateMachine, LoadingCurtain loadingCurtain, SceneLoader sceneLoader,
			IUiFactory uiFactory, IPlayerFactory playerFactory)
		{
			_loadingCurtain = loadingCurtain;
			_uiFactory = uiFactory;
			_sceneLoader = sceneLoader;
			_stateMachine = stateMachine;
			_playerFactory = playerFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, onLoaded: OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
		}

		private void InitGameWorld()
		{
			var character = _playerFactory.CreateHero(GameObject.FindWithTag(InitialPoint));
			_uiFactory.CreateHUD();

			InitCameraFollow(character);
		}

		private void InitCameraFollow(GameObject character)
		{
			Camera.main
				.GetComponent<CameraFollower>()
				.Follow(character);
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}
}