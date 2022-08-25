namespace CodeBase.Logic.Friends
{
    public static class FriendExtensions
    {
        public static int GetBackFriendsChainCount(this IFriend friend)
        {
            return friend.BackFriend?.GetBackFriendsChainCount() ?? 1;
        }
    }
}