using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Infrastructer
{
    //Машина состояний игры
    public class GameStateMachine
    {
        //Возможные состояния
        private readonly Dictionary<Type, IExitableState> _states;
        //Текущее состояние
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader)
            };
        }
        //Вход в состояние без входных параметров
        public void Enter<TState>() where TState : class, IState
        {
            IState _state = ChangeState<TState>();
            _state.Enter();
        }

        //Вход в состояние с входными параметрами
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState _state = ChangeState<TState>();
            _state.Enter(payload);
        }
        
        //Смена одного состояния на другое
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            //Если текущее состояние не пустое - выходим из него
            _activeState?.Exit();
            TState _state = GetState<TState>();
            _activeState = _state;
            return _state;
        }
        //даункаст текущего состояния в TState(чтобы работало для любого из состояний)
        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
    
    
}