using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendChainElement : MonoBehaviour, IFriend
    {
        public Vector3 Position => transform.position;
        public IFriend NextFriend { get; set; }
        public IFriend BackFriend { get; set; }

        public IFriend GetLastFriend
        {
            get
            {
                if (BackFriend == null)
                    return this;
                else
                    return BackFriend.GetLastFriend;
            }
        }

        public void AddBackFriend(IFriend friend)
        {
            var lastFriend = GetLastFriend;
            lastFriend.BackFriend = friend;
            friend.NextFriend = lastFriend;
        }
    }
}