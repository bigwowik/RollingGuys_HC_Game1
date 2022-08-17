using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string nextScene, Action onLoad = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(nextScene, onLoad));

        
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            Debug.Log($"[SceneLoader] Start Load Scene: {nextScene}");
            
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                Debug.Log($"[SceneLoader] Try to start Scene that is nextScene: {nextScene}");
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