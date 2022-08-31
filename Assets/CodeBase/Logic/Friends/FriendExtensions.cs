namespace CodeBase.Logic.Friends
{
    public static class FriendExtensions
    {
        public static int GetBackFriendsChainCount(this FriendChain friend)
        {
            return friend.Previous != null ?
                friend.Previous.GetBackFriendsChainCount() + 1 : 1;
        }
    }
}