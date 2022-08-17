using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner //entry point
    {
        public LoadingFader FaderPrefab;
        
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(FaderPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
        
        
    }
}