using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Friends
{
    public class Coin : TriggerInteractiveBase<FriendChain>
    {
        [SerializeField] private int CoinCount = 1;
        private ILevelProgressService _levelProgressService;

        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }
        protected override void OnTriggerAction(GameObject triggerObject)
        {
            _levelProgressService.AddCoin(CoinCount);
            Destroy(gameObject);
        }
    }
}