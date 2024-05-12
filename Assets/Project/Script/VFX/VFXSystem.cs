using DG.Tweening;
using Gazeus.DesafioMatch3.Controllers;
using Gazeus.DesafioMatch3.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gazeus.DesafioMatch3
{
    public class VFXSystem : MonoBehaviour
    {

        #region Variables

        public List<PlayVFX> _vfxQueu;

        public static Event OnSequenceFinished = new Event();
        public static Action<VFXType,Vector2> AddVFXToQueu;
        public static Action<int> PlayVFXQueue;
        public static Action<VFXType, Vector2> PlaySingleVFX;

        #endregion

        #region Unity

        private void OnEnable()
        {
            GameController.OnBoardAnimationFinished.AddListener(ClearQueue);
            AddVFXToQueu += AddVFX;
            PlayVFXQueue += PlayFromQueu;
        }

        private void OnDisable()
        {
            GameController.OnBoardAnimationFinished.RemoveListener(ClearQueue);
            AddVFXToQueu -= AddVFX;
        }

        #endregion

        #region Methods

        public void AddVFX(VFXType type, Vector2 position)
        {
            _vfxQueu.Add(new PlayVFX()
            {
                _type = type,
                _position = position
            });
        }

        public void PlayFromQueu(int position)
        {
            VFXPoolingSystem.SpawnVFX(_vfxQueu[position]._type, _vfxQueu[position]._position);
            _vfxQueu.RemoveAt(position);
        }

        public void PlayEntireQueue()
        {
            for (int i = 0; i < _vfxQueu.Count; i++)
            {
                VFXPoolingSystem.SpawnVFX(_vfxQueu[i]._type, _vfxQueu[i]._position);
            }
        }

        private void ClearQueue()
        {
            _vfxQueu.Clear();
        }

        #endregion
    }
    
    public struct PlayVFX
    {
        public VFXType _type { get; set; }
        public Vector2 _position { get; set; }
    }

    public class VFXQueu
    {
        
    }
}
