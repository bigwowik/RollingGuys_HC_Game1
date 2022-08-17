using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.SceneLoading
{
    public class SceneLoadOnStart : MonoBehaviour
    {
        [SerializeField] private string _loadLevelName;
        [SerializeField] private float _timeDelay;


        private IGameStateMachine _gameStateMachine;

        private void Construct()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
            Construct();
        
            StartCoroutine(LoadAfterTime(_loadLevelName, _timeDelay));
        }

        private IEnumerator LoadAfterTime(string levelName, float time)
        {
            yield return new WaitForSeconds(time);
            _gameStateMachine.Enter<LoadLevelState, string>(levelName);
        }
    }
}
