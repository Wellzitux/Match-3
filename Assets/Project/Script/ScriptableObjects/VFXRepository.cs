using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    [CreateAssetMenu(fileName ="VFXRepository", menuName ="VFX/VFXRepository", order =0)]
    public class VFXRepository : ScriptableObject
    {

        #region Variables

        public List<VFX> _particleObject; 

        #endregion


    }

    [Serializable]
    public struct VFX
    {
        public VFXType _type;
        public ParticleSystem _particleSystemPrefab;
    }

    public enum VFXType { Explosion, MatchVFX }
}
