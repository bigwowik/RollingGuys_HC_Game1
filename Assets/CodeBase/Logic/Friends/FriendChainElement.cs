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

        public void AddFriend(IFriend friend)
        {
            if (BackFriend != null)
                BackFriend.NextFriend = friend;


            friend.NextFriend = this;
            friend.BackFriend = BackFriend;
            
            BackFriend = friend;
            
            // var lastFriend = GetLastFriend;
            // lastFriend.BackFriend = friend;
            // friend.NextFriend = lastFriend;
        }

        public void RemoveMe()
        {
            if (NextFriend == null)//if first element
            {
                if (BackFriend != null)
                {
                    GetComponent<Rigidbody>().position = BackFriend.Position;
                    BackFriend.RemoveMe();

                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                //if not first element

                if (BackFriend != null)
                {
                    NextFriend.BackFriend = BackFriend;
                    BackFriend.NextFriend = NextFriend;
                }
                Destroy(gameObject);
            }


        }
    }
}