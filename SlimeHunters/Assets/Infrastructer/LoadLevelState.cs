using UnityEngine;

namespace Infrastructer
{
    //Состояние загрузки уровня
    public class LoadLevelState : IPayloadedState<string>
    {
        //Машина состояний
        private readonly GameStateMachine _stateMachine;
        //Загрузчик сцен
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        //Загрузка сцены по имени
        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
            
        }
        
        //Что нужно загрузить
        private void OnLoaded()
        {
            
        }

        //Инициализация объекта на сцене
        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}