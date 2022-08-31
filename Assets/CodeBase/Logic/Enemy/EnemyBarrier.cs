using CodeBase.Logic.Friends;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyBarrier : TriggerInteractiveBase<FriendChain>
    {
        [SerializeField] private bool DestroyAfterTrigger = false;
        protected override void OnTriggerAction(GameObject triggerObject)
        {
            triggerObject.GetComponent<FriendChain>().RemoveMe();
            
            if(DestroyAfterTrigger) 
                Destroy(gameObject);
        }
    }
}