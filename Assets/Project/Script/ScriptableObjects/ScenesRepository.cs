using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    [CreateAssetMenu(fileName ="SceneRepository", menuName ="ScriptableObjects/Scenes/SceneRepository", order =0)]
    public class ScenesRepository : ScriptableObject
    {
        #region Variables

        public List<SceneList> _scenesRepository;
        
        #endregion
    }

    [Serializable]
    public struct SceneList
    {
        public string _sceneName;
        public SceneListEnum.LoadableScenes _sceneType;
    }
}
