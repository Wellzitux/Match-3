using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3.Views
{
    public class TileSpotView : MonoBehaviour
    {
        public event Action<int, int> Clicked;
        public event Action<Transform> Active;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TweenData _tweenData;
        [SerializeField] private Button _button;
        private Transform _tileVisual;

        private int _x;
        private int _y;

        #region Unity

        private void Awake()
        {
            _button.onClick.AddListener(OnTileClick);
        }

        #endregion

        #region Methods

        public Tween AnimatedSetTile(GameObject tile)
        {
            tile.transform.SetParent(transform);
            tile.transform.DOKill();

            return tile.transform.DOMove(transform.position, 0.3f);
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void SetTile(GameObject tile)
        {
            tile.transform.SetParent(transform, false);
            tile.transform.position = transform.position;
            _tileVisual = transform;

        }

        private void OnTileClick()
        {
            Clicked?.Invoke(_x, _y);
            Active?.Invoke(_tileVisual);
        }


        #endregion
    }
}
