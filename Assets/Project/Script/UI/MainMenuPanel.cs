using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class MainMenuPanel : MonoBehaviour
    {

        #region Variables

        [Header("Menu Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;

        #endregion

        #region Unity Messages

        private void OnEnable()
        {

            _playButton.onClick.AddListener(PlayGame);
            _optionsButton.onClick.AddListener(OpenOptions);

        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
            _optionsButton.onClick.RemoveAllListeners();
        }

        #endregion

        #region Methods

        private void PlayGame()
        {
            TransitionController.StartTransition(SceneListEnum.LoadableScenes.GAMEPLAY);
        }

        private void OpenOptions()
        {
            OptionsPanel.OpenOptions?.Invoke(true);
        }

        #endregion

    }
}
