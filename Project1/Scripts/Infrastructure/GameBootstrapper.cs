using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
	public class GameBootstrapper : MonoBehaviour
	{
		private IGameStateMachine _stateMachine;

		[Inject]
		private void Construct(IGameStateMachine stateMachine) =>
			_stateMachine = stateMachine;

		private void Start()
		{
			_stateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}

		public class Factory : PlaceholderFactory<GameBootstrapper>
		{
		}
	}
}