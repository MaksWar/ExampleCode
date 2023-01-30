using Infrastructure;
using Zenject;

namespace Infrastructure.States
{
	public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
			Container.BindFactory<IGameStateMachine, LoadProgressState, LoadProgressState.Factory>();
			Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
			Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();

			Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
		}
	}
}