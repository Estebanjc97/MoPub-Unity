using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoPubApplication
{
    public class MoPubController : MonoBehaviour
    {

        #region Properties

        [SerializeField]
        private string interstitialUnitId, rewardedUnitId;
        [SerializeField]
        private UIManager uiManager;

        private int rewards;

        #endregion

        #region Unity Functions

        private void Start()
        {
            rewards = 0;
        }

        #endregion

        #region Class Functions

        public void InitMoPub()
        {
            MoPub.InitializeSdk(MoPubManager.Instance.SdkConfiguration);
            MoPub.LoadInterstitialPluginsForAdUnits(new string[1] { interstitialUnitId });
            MoPub.LoadRewardedVideoPluginsForAdUnits(new string[1] { rewardedUnitId });
            MoPubManager.OnSdkInitializedEvent += SdkInitialized;
            if (uiManager)
                uiManager.SetEventDebug("Initializing sdk...", Color.white);
        }

        public void LoadInterstitial()
        {
            MoPub.RequestInterstitialAd(interstitialUnitId);
            MoPubManager.OnInterstitialLoadedEvent += InterstitialLoaded;
            MoPubManager.OnInterstitialFailedEvent += InterstitialFailed;
            
            if (uiManager)
                uiManager.SetEventDebug("Requesting intestitial...", Color.white);
        }

        public void LoadRewarded()
        {
            MoPub.RequestRewardedVideo(rewardedUnitId);
            MoPubManager.OnRewardedVideoLoadedEvent += RewardedVideoLoaded; 
            MoPubManager.OnRewardedVideoFailedEvent += RewardedVideoFailed; 
            
            if (uiManager)
                uiManager.SetEventDebug("Requesting rewarded video...", Color.white);
        }

        public void ShowInterstitial()
        {
            MoPub.ShowInterstitialAd(interstitialUnitId);
            MoPubManager.OnInterstitialDismissedEvent += InsterstitialDimissed;
            MoPubManager.OnInterstitialShownEvent += InsterstitialShown;
            MoPubManager.OnInterstitialClickedEvent += InsterstitialClicked;
        }

        public void ShowRewarded()
        {
            if (MoPub.HasRewardedVideo(rewardedUnitId))
            {
                MoPub.ShowRewardedVideo(rewardedUnitId);
                MoPubManager.OnRewardedVideoShownEvent += RewardedVideoShown;
                MoPubManager.OnRewardedVideoFailedToPlayEvent += RewardedVideoFailed;
                MoPubManager.OnRewardedVideoReceivedRewardEvent += RecibeReward;
            }
            else
            {
                Debug.LogError("The rewarded video did not load");
                if (uiManager)
                    uiManager.SetEventDebug("The rewarded video did not load", Color.red);
            }
        }

        private void SdkInitialized(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug("The SDK was initialized", Color.white);

            GameManager.Instance.ChangeState(1);
        }

        private void InterstitialLoaded(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug("Interstitial was loaded", Color.white);

            GameManager.Instance.ChangeState(2);
        }

        private void InsterstitialDimissed(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Interstitial {0} was Dimissed", adUnitId), Color.yellow);
        }

        private void InsterstitialShown(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Interstitial {0} shown", adUnitId), Color.white);
            GameManager.Instance.ChangeState(0);
            MoPub.DestroyInterstitialAd(adUnitId);
        }

        private void InsterstitialClicked(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Interstitial {0} was clicked", adUnitId), Color.green);
        }

        private void InterstitialFailed(string adUnitId, string error)
        {
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Interstitial {0} failed, reason: {1}", adUnitId, error), Color.red);
        }


        private void RewardedVideoLoaded(string adUnitId)
        {
            if (uiManager)
                uiManager.SetEventDebug("Rewarded Video was loaded", Color.white);

            GameManager.Instance.ChangeState(3);
        }

        private void RewardedVideoShown(string adUnitId)
        {
            
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Interstitial {0} shown", adUnitId), Color.white);
           
            GameManager.Instance.ChangeState(0);
        }

        private void RecibeReward(string adUnitId, string key, float value)
        {
            if (adUnitId == rewardedUnitId)
            {
                rewards++;
                if (uiManager)
                    uiManager.SetERewardsCounter(rewards);
                MoPubManager.OnRewardedVideoReceivedRewardEvent -= RecibeReward;
            }
            Debug.Log(string.Format("{0}  --  {1}  --  {2}", adUnitId, key, value)); 
        }

        private void RewardedVideoFailed(string adUnitId, string error)
        {
            if (uiManager)
                uiManager.SetEventDebug(string.Format("Rewarded video {0} failed, reason: {1}", adUnitId, error), Color.red);
        }

        #endregion
    }
}

