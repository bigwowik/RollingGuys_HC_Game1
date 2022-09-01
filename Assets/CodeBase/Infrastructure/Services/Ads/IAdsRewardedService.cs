using System;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public interface IAdsRewardedService
    {
        event Action OnAdsLoaded;
        bool IsAdsLoaded { get; }
        void ShowAd();
        event Action<ShowResult> OnAdsShowCompleted;
        void Init();
    }
}