using System;
using System.Collections.Generic;
using CodeBase.Helpers.Debug;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly DiContainer _container;
        private IExitableState _activeState;

        public GameStateMachine(DiContainer container)
        {
            _container = container;
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            WDebug.Log($"Enter to {typeof(TState).ToString()}", WType.GameStates);
            state.Enter();
        }
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            WDebug.Log($"Enter to: {typeof(TState).ToString()} with payload: {payload.ToString()}", WType.GameStates);
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _container.Resolve<TState>() as TState;
        }
    }
}