using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendInteractive : TriggerInteractiveBase<IFriend>
    {
        public FriendMovement FriendMovement;
        
        private IFriend _friend;

        private void Awake()
        {
            _friend = GetComponent<IFriend>();
        }

        protected override void OnTriggerAction(GameObject triggerObject)
        {
            FriendMovement.enabled = true;
            triggerObject.GetComponent<IFriend>().AddFriend(_friend);
        }
    }
}