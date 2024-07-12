using Infrastructer;
using UnityEngine;

//Объект, ответственный за запуск игры
public class GameBootStrapper : MonoBehaviour, ICoroutineRunner
{
    private Game _game;

    private void Awake()
    {
        _game = new Game(this);
        //Входим в состояние запуска игры
        _game.StateMachine.Enter<BootstrapState>();
        //Не уничтожаем объект при смене сцены
        DontDestroyOnLoad(this);
    }
}