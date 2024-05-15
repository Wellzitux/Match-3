using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine.UI;
namespace Gazeus.DesafioMatch3
{
    public class MenuButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        #region Variables

        [SerializeField] private Button _buttonComponent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TweenData _tweenData;

        [SerializeField] private UnityEvent _OnButtonClicked = new UnityEvent();

        Sequence _onEnterSequence;
        Tween _enterSizeDeltaTween;
        Tween _scaleTween;
        Tween _blinkTween;

        #endregion

        #region Unity

        private void OnEnable()
        {
            if (_buttonComponent)
            {
                _buttonComponent.onClick.AddListener(OnButtonClick);
            }
        }

        private void OnDisable()
        {
            if (_enterSizeDeltaTween != null)
            {
                _onEnterSequence.Kill();
            }
            if (_scaleTween != null)
            {
                _scaleTween.Kill();
            }

            if (_buttonComponent)
            {
                _buttonComponent.onClick.RemoveListener(OnButtonClick);
            }
        }

        #endregion

        #region Methods
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_enterSizeDeltaTween == null)
            {
                _enterSizeDeltaTween = DOTween.To(() => _rectTransform.sizeDelta, x => _rectTransform.sizeDelta = x,
                    _tweenData._finalSizeDelta,
                    _tweenData._sizeDeltaTweenDuration)
                    .SetEase(_tweenData._sizeDeltaAnimationCurve)
                    .SetAutoKill(false);

                _scaleTween = _rectTransform.DOScale(
                    _tweenData._finalScale, _tweenData._scaleTweenDuration)
                    .SetEase(_tweenData._scaleAnimationCurve)
                    .SetAutoKill(false);
            }
            else
            {
                _enterSizeDeltaTween.PlayForward();
                _scaleTween.PlayForward();
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _enterSizeDeltaTween.PlayBackwards();
            _scaleTween.PlayBackwards();
        }

        public void OnButtonClick()
        {
            _OnButtonClicked?.Invoke();
 

            _rectTransform.DOPunchScale(
                    Vector3.one*0.5f, _tweenData._scaleTweenDuration,10,15);
        }

        #endregion

    }
}
