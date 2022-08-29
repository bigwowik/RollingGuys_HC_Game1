using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public interface IFriend
    {
        Vector3 Position { get; }
        IFriend NextFriend { get; set; }
        IFriend BackFriend { get; set; }
        IFriend GetLastFriend { get; }
        void AddFriend(IFriend friend);
        void RemoveMe();
        GameObject GetGameObject { get; }
    }
}
