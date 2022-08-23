using System;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendDestructible : MonoBehaviour
    {
        private IFriend _friend;

        private void Awake()
        {
            _friend = GetComponent<IFriend>();
        }

        public void DestroyMe()
        {
            _friend.RemoveMe();
        }
    }
}