namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        //private const string SceneName = "Level1";
        
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter() => 
            _gameStateMachine.Enter<LoadDataState>();

        public void Exit()
        {
            
        }
    }
}