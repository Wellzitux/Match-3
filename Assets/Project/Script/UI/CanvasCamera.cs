using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasCamera : MonoBehaviour
    {

        #region Variables

        [SerializeField] bool _changeCanvasLayer;
        
        #endregion

        #region Unity

        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (!canvas.worldCamera)
                canvas.worldCamera = Camera.main;

            if (_changeCanvasLayer)
            {
                canvas.sortingLayerName = "Foreground";
            }
        }

        #endregion

    }
}
