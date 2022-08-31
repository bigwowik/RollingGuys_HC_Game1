using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Friends;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Enemy
{
    public class EndLevelTrigger : TriggerInteractiveBase<FriendChain>
    {
        private ILevelProgressService _levelProgressService;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }
        
        protected override void OnTriggerAction(GameObject triggerObject)
        {
            var endLevelType = triggerObject.GetComponent<FriendChain>().GetBackFriendsChainCount() > 1 
                ? LevelResult.WIN 
                : LevelResult.FAIL;

            _levelProgressService.EndLevelTriggerAction(endLevelType);
        }
    }
}