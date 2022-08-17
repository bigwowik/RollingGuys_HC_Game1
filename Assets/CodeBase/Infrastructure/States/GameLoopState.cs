namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
        }

        public void Enter()
        {
            //Helper.MouseLock(true);
        }

        public void Exit()
        {
            //Helper.MouseLock(false);
        }
    }
}