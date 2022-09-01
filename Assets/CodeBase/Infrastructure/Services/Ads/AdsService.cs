using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdUnitId = "Interstitial_Android";
        private const string IosAdUnitId = "Interstitial_iOS";
        private string _adUnitId;

        public void Init()
        {
            SelectPlatform();
            LoadAd();
        }

        public void SelectPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _adUnitId = AndroidAdUnitId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _adUnitId = IosAdUnitId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _adUnitId = AndroidAdUnitId;
                    break;
                default:
                    Debug.Log("Unsupported platform");
                    break;
            }
        }

        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log($"Loading Ad Unit was success: {adUnitId}");
            // Optionally execute code if the Ad Unit successfully loads content.
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
        }
    }
}
