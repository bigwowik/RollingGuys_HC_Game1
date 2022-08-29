using Cinemachine;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class FriendChainElement : MonoBehaviour, IFriend
    {
        private CinemachineVirtualCamera _camera;
        private IGameFactory _gameFactory;
        private ILevelProgressService _levelProgressService;
        public Vector3 Position => transform.position;
        public IFriend NextFriend { get; set; }
        public IFriend BackFriend { get; set; }

        
        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }
        
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
        }

        public void RemoveMe()
        {
            if (NextFriend == null) //if first element
            {
                if (BackFriend != null)
                {
                    var back = BackFriend.GetGameObject;
                    back.GetComponent<Hero>().enabled = true;
                    back.GetComponent<FriendMovement>().enabled = false;
                    
                    //hero.enabled = false;
                    Destroy(gameObject);
                    BackFriend.NextFriend = null;
                    
                    _levelProgressService.ActivePlayer = back.transform;
                    _levelProgressService.PlayerCamera.Follow = back.transform;
                    return;
                }
                else
                {
                    Destroy(gameObject);

                    
                    _levelProgressService.ReloadLevel();
                    
                    
                    
                    return;
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

        public GameObject GetGameObject => gameObject;
        
    }
}