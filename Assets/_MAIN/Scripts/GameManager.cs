using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esteban.Utils.Events;

namespace MoPubApplication
{
    public class GameManager : Sirenix.OdinInspector.SerializedMonoBehaviour
    {
        #region Properties

        private AppStates currentAppSatate = AppStates.InitializeMoPub;

        public DynamicIntEvent OnAppStateChange;

        public static GameManager Instance
        {
            get; private set;
        }

        #endregion

        #region Unity Functions

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            else
                Instance = this;

            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            ShareCurrentState();
        }

        #endregion

        #region Class Functions

        public void ChangeState(int newStateIndex)
        {
            currentAppSatate = (AppStates)newStateIndex;
            ShareCurrentState();
        }

        private void ShareCurrentState()
        {
            switch (currentAppSatate)   
            {
                case AppStates.InitializeMoPub:
                    OnAppStateChange?.Invoke(0);
                    break;
                case AppStates.LoadAds:
                    OnAppStateChange?.Invoke(1);
                    break;
                case AppStates.ShowInterstitial:
                    OnAppStateChange?.Invoke(2);
                    break;
                case AppStates.ShowRewardedVideo:
                    OnAppStateChange?.Invoke(3);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}

