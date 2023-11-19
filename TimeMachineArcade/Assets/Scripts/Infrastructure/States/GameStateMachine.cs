using System;
using System.Collections.Generic;
using Infrastructure.Logic;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadProgressState)] = new LoadProgressState(this, progressService, saveLoadService),
                [typeof(LoadRoomSceneState)] = new LoadRoomSceneState(this, sceneLoader, curtain),
                [typeof(LoadLevelSceneState)] = new LoadLevelSceneState(this, sceneLoader, curtain),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(RoomLoopState)] = new RoomLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}