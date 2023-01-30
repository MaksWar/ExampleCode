using Zenject;

namespace Infrastructure.States
{
	public class GameLoopState : IState
	{
		public GameLoopState()
		{
		}

		public void Enter()
		{
		}

		public void Exit()
		{
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
		{}
	}
}