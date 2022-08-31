using CodeBase.Logic.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendInteractive : TriggerInteractiveBase<FriendChain>
    {
        public MonoBehaviour FriendMovement;
        
        //components
        private FriendChain _friend;

        private bool _wasActivated;
        

        private void Awake() => 
            _friend = GetComponent<FriendChain>();

        protected override void OnTriggerAction(GameObject triggerObject)
        {

            if(_wasActivated) return;
            _wasActivated = true;
            
            FriendMovement.enabled = true;
            triggerObject.GetComponent<FriendChain>().AddFriend(_friend);
            
        }
    }
}