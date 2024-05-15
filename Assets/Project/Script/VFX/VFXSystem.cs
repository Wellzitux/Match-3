using System;
using System.Collections.Generic;
using UnityEngine;

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
            AddVFXToQueu += AddVFX;
            PlayVFXQueue += PlayFromQueu;
            PlaySingleVFX += PlayVFX;
        }

        private void OnDisable()
        {
            AddVFXToQueu -= AddVFX;
            PlaySingleVFX -= PlayVFX;

        }

        #endregion

        #region Methods

        public void PlayVFX(VFXType type, Vector2 position)
        {
            VFXPoolingSystem.SpawnVFX(type, position);
        }

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

        #endregion
    }

    #region Struct

    public struct PlayVFX
    {
        public VFXType _type { get; set; }
        public Vector2 _position { get; set; }
    }

    #endregion
}
