using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region Variables

        public static T Instance;

        #endregion

        #region Unity Messages

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this as T;
            DontDestroyOnLoad(this);
        }

        #endregion
    }
}
