using CodeBase.Logic.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.Logic.Friends
{
    public class FriendInteractive : TriggerInteractiveBase<IFriend>
    {
        public CharacterView CharacterView;
        
        private IFriend _friend;

        private void Awake()
        {
            _friend = GetComponent<IFriend>();
        }

        protected override void OnTriggerAction(GameObject triggerObject)
        {
            CharacterView.enabled = true;
            triggerObject.GetComponent<IFriend>().AddFriend(_friend);
        }
    }
    
    public class FriendInteractive2
    {
        private IFriend _friend;


        public FriendInteractive2(GameObject gameObject)
        {
            gameObject.OnTriggerEnterAsObservable()
                .Where(c => c.TryGetComponent<IFriend>(out var friend))
                .Take(1)
                .Subscribe(c => OnTriggerAction(c.gameObject));
            
            
        }

        protected void OnTriggerAction(GameObject triggerObject)
        {
            triggerObject.GetComponent<IFriend>().AddFriend(_friend);
        }
    }
}