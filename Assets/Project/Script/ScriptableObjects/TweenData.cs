using UnityEngine;

namespace Gazeus.DesafioMatch3
{

    [CreateAssetMenu(fileName ="Tween Data", menuName ="Button/Tween Data", order =0)]
    public class TweenData : ScriptableObject
    {
        #region Variables

        [Header("Size/Delta Tween")]
        [SerializeField] public Vector2 _finalSizeDelta;
        [SerializeField] public float _sizeDeltaTweenDuration;
        [SerializeField] public AnimationCurve _sizeDeltaAnimationCurve;

        [Header("Scale Tween")]
        [SerializeField] public Vector3 _finalScale;
        [SerializeField] public float _scaleTweenDuration;
        [SerializeField] public AnimationCurve _scaleAnimationCurve;

        [Header("Blink Tween")]
        [SerializeField] public Color32 _color;
        [SerializeField] public float _colorTweenDuration;
        [SerializeField] public AnimationCurve _colorAnimationCurve;

        [Header("Movement Tween")]
        [SerializeField] public Vector3 _movementVector;
        [SerializeField] public float _movementTweenDuration;
        [SerializeField] public AnimationCurve _movementAnimationCurve;

        [Header("CanvasGroup Tween")]
        [SerializeField] public float _alphaValue;
        [SerializeField] public float _alphaValueTweenDuration;
        [SerializeField] public AnimationCurve _alphaValueAnimationCurve;

        [Header("Rotation Tween")]
        [SerializeField] public Vector3 _rotationValue;
        [SerializeField] public float _rotationTweenDuration;
        [SerializeField] public AnimationCurve _rotationAnimationCurve;

        [Header("Anchored Position Tween")]
        [SerializeField] public Vector2 _anchoredPositionValue;
        [SerializeField] public float _anchoredPositionTweenDuration;
        [SerializeField] public AnimationCurve _anchoredPositionAnimationCurve;

        #endregion
    }
}
