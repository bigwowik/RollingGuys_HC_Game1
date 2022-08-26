using UnityEngine;

namespace CodeBase.Logic.Friends
{
    [CreateAssetMenu(fileName = "FriendConfig", menuName = "Configs/FriendConfig", order = 1)]
    public class FriendConfig : ScriptableObject
    {
        public float CriticalDistance = 1f;
        public float Speed = 1f;
    }
}