using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Infrastructer
{
    public class SceneLoader
    {
        //Корутино-запускатель ._ .
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        //Загрузка сцены
        public void Load(string name, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        //Проверяем, чтобы сцена не могла загружать саму себя
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();

        }
    }
}