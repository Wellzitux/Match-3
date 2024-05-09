using System;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class OptionsPanel : Singleton<OptionsPanel>
    {

        #region Variables

        [SerializeField] private Canvas _optionsPanelCanvas;
        [SerializeField] private RectTransform _optionsPanel;

        public static Action<bool> OpenOptions;
        public static Event OnOptionsOpen = new Event();
        public static Event OnOptionsClosed = new Event();

        #endregion

        #region Unity Messages

        private void OnEnable()
        {
            OpenOptions += ToggleOptions;
        }

        private void OnDisable()
        {
            OpenOptions -= ToggleOptions;
        }

        #endregion

        #region Methods

        private void ToggleOptions(bool state)
        {

            ToggleCanvas(true);

        }

        private void ToggleCanvas(bool state) => _optionsPanelCanvas.enabled = state;

        #endregion

    }
}
