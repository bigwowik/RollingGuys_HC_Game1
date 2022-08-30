using CodeBase.Infrastructure.Services.Progress;
using CodeBase.UI.Windows.EndLevelWindow;

namespace CodeBase.Infrastructure.States
{
    public class RewardService : IRewardService
    {
        public const float AdsRewardValue = 3f;
        
        private readonly IProgressService _progressService;
        private readonly IMediator _mediator;
        private readonly RewardService _rewardService;

        public RewardService(IProgressService progressService, IMediator mediator)
        {
            _progressService = progressService;
            _mediator = mediator;
        }

        public void GiveClassicReward(EndLevelData endLevelData)
        {
            var coinsToAdd = endLevelData.CollectedCoins;
            AddCoins(coinsToAdd);
            
        }

        public void GiveAdsReward(EndLevelData endLevelData)
        {
            var coinsToAdd = (int) (endLevelData.CollectedCoins * AdsRewardValue);
            AddCoins(coinsToAdd);
        }

        private void AddCoins(int coinsToAdd)
        {
            _progressService.ProgressData.ResourcesData.Coins += coinsToAdd;
            _progressService.Save();
            
            _mediator.SetCoinsText(_progressService.ProgressData.ResourcesData.Coins.ToString());
        }
    }
}