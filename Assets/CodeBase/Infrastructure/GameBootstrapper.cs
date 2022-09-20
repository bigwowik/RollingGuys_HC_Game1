using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        [Inject]
        private void Construct(Game game) => 
            _game = game;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _game.GameStateMachine.Enter<BootstrapState>();
            //DontDestroyOnLoad(this);

        }
    }
}