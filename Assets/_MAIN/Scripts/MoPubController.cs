using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoPubApplication
{
    public class MoPubController : MonoBehaviour
    {

        [SerializeField]
        private string interstitialUnitId, rewardedUnitId;
        [SerializeField]
        private UIManager uiManager;

        public void InitMoPub()
        {
            MoPub.InitializeSdk(MoPubManager.Instance.SdkConfiguration);
            MoPub.LoadInterstitialPluginsForAdUnits(new string[1] { interstitialUnitId });
            MoPub.LoadRewardedVideoPluginsForAdUnits(new string[1] { rewardedUnitId });
            MoPubManager.OnSdkInitializedEvent += SdkInitialized;
            if (uiManager)
                uiManager.SetEventDebug("Initializing sdk...", Color.white);
        }

        private void SdkInitialized(string message)
        {
            if (uiManager)
                uiManager.SetEventDebug("The SDK was initialized", Color.white);

            GameManager.Instance.ChangeState(1);
        }

        public void LoadInterstitial()
        {
            MoPub.RequestInterstitialAd(interstitialUnitId);
            MoPubManager.OnInterstitialLoadedEvent += InterstitialLoaded;
            if (uiManager)
                uiManager.SetEventDebug("Requesting intestitial...", Color.white);
        }

        private void InterstitialLoaded(string message)
        {
            if (uiManager)
                uiManager.SetEventDebug("Interstitial was loaded", Color.white);

            GameManager.Instance.ChangeState(2);
        }

        public void LoadRewarded()
        {
            MoPub.RequestRewardedVideo(rewardedUnitId);
            MoPubManager.OnRewardedVideoLoadedEvent += RewardedVideoLoaded;
            if (uiManager)
                uiManager.SetEventDebug("Requesting rewarded video...", Color.white);
        }

        private void RewardedVideoLoaded(string message)
        {
            if (uiManager)
                uiManager.SetEventDebug("Rewarded Video was loaded", Color.white);

            GameManager.Instance.ChangeState(3);
        }

        public void ShowInterstitial()
        {
            MoPub.ShowInterstitialAd(interstitialUnitId);
        }

        public void ShowRewarded()
        {
            if (MoPub.HasRewardedVideo(rewardedUnitId))
            {
                MoPub.ShowRewardedVideo(rewardedUnitId);
            }
            else
            {
                Debug.LogError("Have not the Rewarded video yet.");
            }
        }
    }
}

