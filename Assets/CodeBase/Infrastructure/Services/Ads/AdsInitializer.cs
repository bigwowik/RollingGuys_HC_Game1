using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsInitializer: IUnityAdsInitializationListener, IAdsInitializer
    {
        private readonly IAdsService _adsService;
        private readonly IAdsRewardedService _adsRewardedService;
        private const string AndroidGameId = "4910455";
        private const string IOSGameId = "4910454";
        private const bool TestMode = true;
        
        private string _gameId;

        public AdsInitializer(IAdsService adsService, IAdsRewardedService adsRewardedService)
        {
            _adsService = adsService;
            _adsRewardedService = adsRewardedService;
        }

        public void InitializeAds()
        {            
            Debug.Log("Unity Ads InitializeAds.");

            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOSGameId
                : AndroidGameId;
            Advertisement.Initialize(_gameId, TestMode, true, this);

            if (Advertisement.isInitialized)
                OnInitializationComplete();
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            //_adsService.Init();
            _adsRewardedService.Init();
        }
 
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}