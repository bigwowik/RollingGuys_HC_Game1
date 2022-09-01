using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsRewardedService : IAdsRewardedService, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsListener
    {
        private const string AndroidAdUnitId = "Rewarded_Android";
        private const string IosAdUnitId = "Rewarded_iOS";


        private string _adUnitId = null; // This will remain null for unsupported platforms
        
        public event Action OnAdsLoaded;
        public event Action<ShowResult> OnAdsShowCompleted;
        
        public bool IsAdsLoaded { get; private set; }

        public void Init()
        {
            SelectPlatform();
            LoadAd();
        }

        private void SelectPlatform()
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
            IsAdsLoaded = false;
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
            
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                IsAdsLoaded = true;
                OnAdsLoaded?.Invoke();
                // // Configure the button to call the ShowAd() method when clicked:
                // _showAdButton.onClick.AddListener(ShowAd);
                // // Enable the button for users to click:
                // _showAdButton.interactable = true;
            }
        }

        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
            Advertisement.AddListener(this);
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            // if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            // {
            //     Debug.Log("Unity Ads Rewarded Ad Completed");
            //     // Grant a reward.
            //     OnAdsShowCompleted?.Invoke();
            //
            //     // Load another ad:
            //     LoadAd();
            // }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady");
        }

        public void OnUnityAdsDidError(string message)
        {
            Debug.Log($"OnUnityAdsDidError");
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debug.Log($"OnUnityAdsDidStart");
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId.Equals(_adUnitId))
            {
                OnAdsShowCompleted?.Invoke(showResult);
                //LoadAd();
                // if (showResult.Equals(ShowResult.Finished))
                // {
                //     Debug.Log("Unity Ads Rewarded Ad Completed");
                //     // Grant a reward.
                //
                //
                //     // Load another ad:
                //     
                // }
            }
            
            Debug.Log($"OnUnityAdsDidFinish "+ showResult);
        }
    }
}