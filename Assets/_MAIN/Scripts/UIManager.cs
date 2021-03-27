using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace MoPubApplication
{
    public class UIManager : SerializedMonoBehaviour
    {
        #region Properties

        [SerializeField, FoldoutGroup("UI elements")]
        private RectTransform[] layouts_views;

        #endregion

        #region Unity Functions

        private void Start()
        {
            Setup();
        }

        #endregion

        #region Class Functions

        public void ListenAppState(int stateId)
        {
            LoadLayout(stateId);
        }

        private void Setup()
        {
            HideAllLayouts();
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

