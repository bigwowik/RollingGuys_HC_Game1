using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneLoading : MonoBehaviour
{
    private IGameStateMachine _gameStateMachine;

    private void Construct()
    {
        _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
    }

    private void Start()
    {
        Construct();


        var sceneToLoad = "";

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            var sceneName = FindSceneWithNoLoader(scene);
            
            if (sceneName != "")
            {
                sceneToLoad = sceneName;
                break;
            }
        }
        StartCoroutine(LoadAfterTime(sceneToLoad, 0));
    }

    private string FindSceneWithNoLoader(Scene scene)
    {
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            if(rootGameObject.GetComponent<TestSceneLoading>())
                return "";
        }

        return scene.name;
    }

    private IEnumerator LoadAfterTime(string levelName, float time)
    {
        yield return new WaitForSeconds(time);
        _gameStateMachine.Enter<LoadLevelState, string>(levelName);
    }
}
