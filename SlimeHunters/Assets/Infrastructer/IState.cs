using System;

namespace Infrastructer
{
    //Интерфейс для состояний без входных параметров
    public interface IState : IExitableState
    {
        void Enter();
    }

    //Интерфейс для состояний с входными параметрами
    public interface IPayloadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
    
    //Интерфейс состояний из которых можно выйти(ну, то есть любого :P)
    public interface IExitableState
    {
        void Exit();
    }
}