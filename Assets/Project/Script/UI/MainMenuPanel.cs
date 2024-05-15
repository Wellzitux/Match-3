using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class MainMenuPanel : MonoBehaviour
    {

        #region Variables

        [Header("Menu Buttons")]
        [SerializeField] private Button _playButton;

        #endregion

        #region Unity Messages

        private void OnEnable()
        {
            _playButton.onClick.AddListener(PlayGame);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
        }

        #endregion

        #region Methods

        private void PlayGame()
        {
            TransitionController.StartTransition(SceneListEnum.LoadableScenes.GAMEPLAY);
        }

        #endregion

    }
}
