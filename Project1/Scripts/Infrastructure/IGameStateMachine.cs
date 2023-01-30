using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;

		void Enter<TState, TPayLoad>(TPayLoad payLoad) where TState : class, IPayLoadedState<TPayLoad>;
	}
}