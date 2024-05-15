using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.DesafioMatch3
{
    public class MainMenuVisual : MonoBehaviour
    {

        #region Variables

        [SerializeField] private List<AnimatedTiledImages>_animatedTiledImages;

        #endregion

        #region Unity

        private void OnEnable()
        {
            foreach (var animatedImage in _animatedTiledImages)
            {
                UpdateMaterialTilingVelocity(animatedImage._animatedImage, animatedImage._animatedVelocity);
            }
        }

        #endregion

        #region Methods

        private void UpdateMaterialTilingVelocity(Image image, float velocity)
        {
            Material instancedMaterial = Instantiate(image.materialForRendering);
            instancedMaterial.SetFloat("_Velocity", velocity);
            image.material = instancedMaterial;
        }

        #endregion

        #region Structs

        [Serializable]
        public struct AnimatedTiledImages
        {
            public Image _animatedImage;
            public float _animatedVelocity;

        }

        #endregion

    }
}
