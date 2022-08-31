using Cinemachine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendChain : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;
        private IGameFactory _gameFactory;
        private ILevelProgressService _levelProgressService;
        public Vector3 Position => transform.position;
        public FriendChain Next { get; set; }
        public FriendChain Previous { get; set; }

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
            
            Destroy(gameObject);
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

        private void Fail() => 
            _levelProgressService.ReloadLevelWithFail();

        private void ChangeActivePlayer(GameObject prev)
        {
            prev.GetComponent<HeroMovement>().enabled = true;
            prev.GetComponent<FriendMovement>().enabled = false;
            
            _levelProgressService.ActivePlayer = prev.transform;
            _levelProgressService.PlayerCamera.Follow = prev.transform;
        }

    }
}