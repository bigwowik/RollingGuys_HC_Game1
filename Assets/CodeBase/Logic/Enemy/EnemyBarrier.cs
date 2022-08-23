using CodeBase.Logic.Friends;
using UnityEngine;

namespace CodeBase.Logic.Enemy
{
    public class EnemyBarrier : TriggerInteractiveBase<IFriend>
    {
        protected override void OnTriggerAction(GameObject triggerObject)
        {
            triggerObject.GetComponent<IFriend>().RemoveMe();
            Destroy(gameObject);
            
        }
    }
}