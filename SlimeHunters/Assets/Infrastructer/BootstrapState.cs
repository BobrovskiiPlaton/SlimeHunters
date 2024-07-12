namespace Infrastructer
{
    public class BootstrapState : IState
    {
        //Сцена, на которой игра запускается
        private const string Initial = "Initial";
        //Машина состояний
        private readonly GameStateMachine _stateMachine;
        //Загрузчик сцены
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        //Входим в состояние запуска игры
        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        //Загрузка сцены
        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Loading");

        //Регестрация сервисов(получение урона, лечения и т.д.)
        private void RegisterServices()
        {
            
        }

        //Выход из состояния
        public void Exit()
        {
            
        }
    }
}