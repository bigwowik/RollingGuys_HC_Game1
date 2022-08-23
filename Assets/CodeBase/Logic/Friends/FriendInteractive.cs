using System;
using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendInteractive : TriggerInteractiveBase<IFriend>
    {
        public FriendMovement FriendMovement;

        protected override void OnTriggerAction(GameObject triggerObject)
        {
            FriendMovement.enabled = true;
            triggerObject.GetComponent<IFriend>().AddFriend(GetComponent<IFriend>());
        }
    }
}