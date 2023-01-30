using System;
using System.Collections.Generic;
using Infrastructure;

namespace Infrastructure.States
{
	public class GameStateMachine : IGameStateMachine
	{
		private IExitableState _activeState;

		private readonly Dictionary<Type, IExitableState> _states;

		public GameStateMachine(BootstrapState.Factory bootstrapState, LoadProgressState.Factory loadProgressState,
			LoadLevelState.Factory loadLevelState)
		{
			_states = new Dictionary<Type, IExitableState>();

			RegisterState(bootstrapState.Create(this));
			RegisterState(loadProgressState.Create(this));
			RegisterState(loadLevelState.Create(this));
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>
		{
			TState state = ChangeState<TState>();
			state.Enter(payLoad);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private void RegisterState<TType>(TType state) where TType : IExitableState =>
			_states[typeof(TType)] = state;

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
}