using CodeBase.UI.Windows.EndLevelWindow;

namespace CodeBase.Infrastructure.States
{
    public interface IRewardService
    {
        void GiveClassicReward(EndLevelData endLevelData);
        void GiveAdsReward(EndLevelData endLevelData);
    }
}