using Infrastructer;

//Класс игры
public class Game
{
    //Машина состояний
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
    }
}