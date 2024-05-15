using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class TileButtonVisual : MonoBehaviour
    {

        #region Variables

        [SerializeField] private Transform _highlight;
        [SerializeField] private TweenData _tweenData;

        private Tween _scale;
        #endregion

        #region Unity

        private void OnDestroy()
        {
            if (_scale!= null)
                    _scale.Kill();
        }
        
        #endregion
        
        #region Methods

        public void SelectedVisual(bool value)
        {
            if (value)
            {
                _scale= _highlight.DOScale(_tweenData._finalScale, _tweenData._scaleTweenDuration)
                    .SetEase(_tweenData._scaleAnimationCurve);
            }
            else
            {
                _scale = _highlight.DOScale(Vector3.one, _tweenData._scaleTweenDuration)
                    .SetEase(_tweenData._scaleAnimationCurve);
            }
        }


        #endregion
    }
}
