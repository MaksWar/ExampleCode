using Infrastructure;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindGameBootstrapperFactory();

			BindAssetProvider();

			BindFactories();

			BindCoroutineRunner();

			BindSceneLoader();

			BindLoadingCoroutine();

			BindGameStateMachine();

			BindPlayerProgressService();

			BindSaveLoadService();

			BindInputService();
		}

		private void BindGameBootstrapperFactory()
		{
			Container
				.BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
				.FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapper);
		}

		private void BindAssetProvider()
		{
			Container
				.BindInterfacesAndSelfTo<AssetProvider>()
				.AsSingle();
		}

		private void BindFactories()
		{
			Container
				.BindInterfacesAndSelfTo<PlayerFactory>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<UiFactory>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<FactoriesContainer>()
				.AsSingle();
		}

		private void BindCoroutineRunner()
		{
			Container
				.Bind<ICoroutineRunner>()
				.To<CoroutineRunner>()
				.FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunner)
				.AsSingle();
		}

		private void BindSceneLoader()
		{
			Container
				.Bind<SceneLoader>()
				.AsSingle();
		}

		private void BindLoadingCoroutine()
		{
			Container
				.Bind<LoadingCurtain>()
				.FromComponentInNewPrefabResource(AssetPath.LoadingCurtain)
				.AsSingle();
		}

		private void BindGameStateMachine()
		{
			Container
				.Bind<IGameStateMachine>()
				.FromSubContainerResolve()
				.ByInstaller<GameStateMachineInstaller>()
				.AsSingle();
		}

		private void BindPlayerProgressService()
		{
			Container
				.BindInterfacesAndSelfTo<PersistentProgressService>()
				.AsSingle();
		}

		private void BindSaveLoadService()
		{
			Container
				.BindInterfacesAndSelfTo<SaveLoadService>()
				.AsSingle();
		}

		private void BindInputService()
		{
			Container
				.Bind<IInputService>()
				.FromMethod(InputService)
				.AsSingle();

			IInputService InputService() =>
				Application.isEditor ? (IInputService) new StandaloneInputService() : new MobileInputService();
		}
	}
}