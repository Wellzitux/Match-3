using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class GameplayCanvasVisual : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float startGameDelay;
        [SerializeField] private Button _homeButton;
        [SerializeField] private TweenData _tweenData;

        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private CanvasGroup _boardPanelCanvasGroup;
        [SerializeField] private RectTransform _boardPanelRectTransform;

        public static Action StartBoard;

        public static Action<Sprite, Transform> SelectTile;
        public static Action ExplosionShake;

        Tween _shakeTween;

        #endregion

        #region Unity

        private void OnEnable()
        {
            StartGameplayCamera();

            StartBoard += StartBoardAnimation;
            ExplosionShake += ExplosionShakeScreen;
            _boardPanelCanvasGroup.alpha = 0;
            _homeButton.onClick.AddListener(GoToMenu);
        }

        private void OnDisable()
        {
            ExplosionShake -= ExplosionShakeScreen;
            StartBoard -= StartBoardAnimation;
            _homeButton.onClick.RemoveListener(GoToMenu);

            if (_shakeTween != null)
                _shakeTween.Kill();

        }

        #endregion

        #region Methods
        private void GoToMenu()
        {
            TransitionController.StartTransition?.Invoke(SceneListEnum.LoadableScenes.MENU);
        }

        private void StartGameplayCamera()
        {
            DOVirtual.DelayedCall(startGameDelay, () => { })
                .OnComplete(()=> GameplayCanvasVisual.StartBoard?.Invoke());
        }

        private void StartBoardAnimation()
        {
            DOTween.To(() => _boardPanelCanvasGroup.alpha, x => _boardPanelCanvasGroup.alpha = x, _tweenData._alphaValue, _tweenData._alphaValueTweenDuration)
                .SetEase(_tweenData._alphaValueAnimationCurve);

            _boardPanelRectTransform.DOScale(_tweenData._finalScale, _tweenData._scaleTweenDuration)
                .SetEase(_tweenData._scaleAnimationCurve)
                .From();

            _boardPanelRectTransform.DORotate(_tweenData._rotationValue, _tweenData._rotationTweenDuration)
                .SetEase(_tweenData._rotationAnimationCurve)
                .From();

                DOTween.To(()=> _boardPanelRectTransform.anchoredPosition, x=> _boardPanelRectTransform.anchoredPosition =x, _tweenData._anchoredPositionValue, _tweenData._movementTweenDuration)
                .SetEase(_tweenData._anchoredPositionAnimationCurve)
                .From();

        }

        private void ExplosionShakeScreen()
        {
            if (_shakeTween != null)
                _shakeTween.Kill();

            _shakeTween = _boardPanelRectTransform.DOPunchRotation(Vector3.one, 0.4f, 10, 0.5f)
                .OnComplete(()=> _boardPanelRectTransform.DORotate(Vector3.zero, 0.2f));
        }


        #endregion
    }
}
