using DG.Tweening;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class GameplayVisualController : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform _camera;
        [SerializeField] private TweenData _tweenData;

        #endregion

        #region Unity

        private void OnEnable()
        {
            StartGameplayCamera();
        }

        #endregion

        private void StartGameplayCamera()
        {
            _camera.DOMove(_tweenData._movementVector, _tweenData._movementTweenDuration)
                .SetEase(_tweenData._movementAnimationCurve)
                .OnComplete(()=> GameplayCanvasVisual.StartBoard?.Invoke());
        }
    }
}
