using System;
using Cinemachine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendChain : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private ILevelProgressService _levelProgressService;
        public Vector3 Position => transform.position;
        public FriendChain Next { get; set; }
        public FriendChain Previous { get; set; }

        public Action OnDeath;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }
        
        public void AddFriend(FriendChain newFriend)
        {
            //actions with prev
            if (Previous != null)
                Previous.Next = newFriend;
            
            //actions with newFriend
            newFriend.Next = this;
            newFriend.Previous = Previous;

            //actions with me
            Previous = newFriend;
        }

        public void RemoveMe()
        {
            if (Next == null)
                RemoveFirstElement();
            else
                RemoveNotFirstElement();

            Death();
        }

        private void RemoveNotFirstElement()
        {
            Next.Previous = Previous;
            
            if (Previous != null) 
                Previous.Next = Next;
        }

        private void RemoveFirstElement()
        {
            if (Previous == null)
                Fail();
            else
            {
                ChangeActivePlayer(Previous.gameObject);
                Previous.Next = null;
            }
        }

        private void Fail()
        {
            // Observable.Timer(TimeSpan.FromSeconds(1.5f))
            //     .Subscribe(_ => _levelProgressService.ReloadLevelWithFail())
            //     .AddTo(this);
            
            _levelProgressService.ReloadLevelWithFail();
        }

        private void ChangeActivePlayer(GameObject prev)
        {
            prev.GetComponent<HeroMovement>().enabled = true;
            prev.GetComponent<FriendMovement>().enabled = false;
            
            _levelProgressService.ActivePlayer = prev.transform;
            _levelProgressService.PlayerCamera.Follow = prev.transform;
        }

        private void Death()
        {
            GetComponent<HeroMovement>().enabled = false;
            
            if(GetComponent<FriendMovement>() != null)
                GetComponent<FriendMovement>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            OnDeath?.Invoke();
            
            //Destroy(gameObject);
        }
    }
}