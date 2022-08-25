using CodeBase.Infrastructure.States;
using CodeBase.Logic.Friends;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Enemy
{
    public class EndLevel : TriggerInteractiveBase<IFriend>
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void OnTriggerAction(GameObject triggerObject)
        {
            var endLevelType = triggerObject.GetComponent<IFriend>().GetBackFriendsChainCount() > 1 
                ? EndLevelType.WIN 
                : EndLevelType.FAIL;

            _gameStateMachine.Enter<EndLevelState, EndLevelType>(endLevelType);
        }
    }
}