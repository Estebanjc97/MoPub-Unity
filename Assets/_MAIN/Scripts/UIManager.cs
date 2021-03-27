using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

namespace MoPubApplication
{
    public class UIManager : SerializedMonoBehaviour
    {
        #region Properties

        [SerializeField, FoldoutGroup("UI elements")]
        private RectTransform[] layouts_views;
        [SerializeField, FoldoutGroup("UI elements")]
        private TextMeshProUGUI events_debugger, rewardeds_counter;

        #endregion

        #region Unity Functions


        #endregion

        #region Class Functions

        public void ListenAppState(int stateId)
        {
            LoadLayout(stateId);
        }

        public void SetEventDebug(string message, Color text_color)
        {
            if (events_debugger != null)
            {
                events_debugger.text = message;
                events_debugger.color = text_color;
            }

            Debug.Log(string.Format("New Event >> {0} >> with color flag >> {1}", message, text_color.ToString())); ;
        }

        public void SetERewardsCounter(int counter)
        {
            if (rewardeds_counter != null)
            {
                rewardeds_counter.text = string.Format("Rewards: {0}", counter);
            }
        }

        public void ReturnToHome()
        {
            GameManager.Instance.ChangeState(0);
        }
        public void ExitApp()
        {
            Application.Quit();
        }

        private void LoadLayout(int index)
        {
            HideAllLayouts();
            layouts_views[index].gameObject.SetActive(true);
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts_views)
            {
                layout.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}

