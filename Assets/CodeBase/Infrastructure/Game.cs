using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingFader loadingFader)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingFader, AllServices.Container);
        }

        
    }
}