using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasCamera : MonoBehaviour
    {

        #region Unity

        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (!canvas.worldCamera)
                canvas.worldCamera = Camera.main;
        }

        #endregion

    }
}
