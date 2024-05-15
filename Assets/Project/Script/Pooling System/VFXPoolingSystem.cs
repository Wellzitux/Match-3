using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class VFXPoolingSystem : MonoBehaviour
    {

        #region Variables

        public static Action<VFXType,Vector3> SpawnVFX;

        public VFXRepository _vfxRepository;

        public Dictionary<string, List<ParticleSystem>> _vfxPool;

        #endregion

        #region Unity

        private void OnEnable()
        {
            _vfxPool = new Dictionary<string, List<ParticleSystem>>();
            InitializeList();

            SpawnVFX += GetVFX;
        }

        private void OnDisable()
        {
            SpawnVFX -= GetVFX;
        }

        #endregion

        #region Methods

        private void InitializeList()
        {
            foreach(VFX vfx in _vfxRepository._particleObject)
            {
                List<ParticleSystem> vfxPool = new List<ParticleSystem>();
                _vfxPool.Add(vfx._type.ToString(), vfxPool);
                ParticleSystem newVFX = Instantiate(vfx._particleSystemPrefab, transform);
                newVFX.gameObject.SetActive(false);
                vfxPool.Add(newVFX);
            }
        }

        private void GetVFX(VFXType type, Vector3 position)
        {
            if (_vfxPool.ContainsKey(type.ToString()))
            {

                List<ParticleSystem> list = _vfxPool[type.ToString()];

                foreach (ParticleSystem vfx in list)
                {
                    if (!vfx.gameObject.activeInHierarchy)
                    {
                        vfx.gameObject.SetActive(true);
                        vfx.GetComponent<RectTransform>().anchoredPosition = position;
                        return;
                    }
                }
                CreateVFX(type, position);
                return;
            }
        }

        private void CreateVFX(VFXType type, Vector3 position)
        {
            foreach (VFX vfx in _vfxRepository._particleObject)
            {
                if (vfx._type.ToString().Equals(type.ToString()))
                {

                    List<ParticleSystem> list = _vfxPool[type.ToString()];

                    ParticleSystem newVFX = Instantiate(vfx._particleSystemPrefab, transform);
                    newVFX.GetComponent<RectTransform>().anchoredPosition = position;
                    list.Add(newVFX);
                }
            }

        }


        #endregion

    }
}
