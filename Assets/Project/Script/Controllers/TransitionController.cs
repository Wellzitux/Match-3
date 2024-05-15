using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Gazeus.DesafioMatch3
{
    public class TransitionController : Singleton<TransitionController>
    {

        #region Variables

        [SerializeField] private Canvas _transitionCanvas;
        [SerializeField] private ScenesRepository _sceneListSO;

        [SerializeField] private Animation _transitionAnimationComponent;
        [SerializeField] private AnimationClip _entranceAnimationClip;
        [SerializeField] private AnimationClip _exitAnimationClip;

        private string _loadingSceneName;
        private Coroutine _transitionRoutine;

        #endregion

        #region Events

        public static Action<SceneListEnum.LoadableScenes> StartTransition;
        public static Event OnTransitionStart = new Event();
        public static Event OnTransitionEnded = new Event();

        #endregion

        #region Unity

        private void OnEnable()
        {
            StartTransition += StartSceneTransition;

            if (_transitionCanvas.enabled == true)
                ToggleCanvas(false);
        }

        private void OnDisable()
        {
            StartTransition -= StartSceneTransition;
        }

        #endregion

        #region Methods

        private void StartSceneTransition(SceneListEnum.LoadableScenes sceneToLoad)
        {
            _loadingSceneName = GetSceneToLoadName(sceneToLoad);

            if (_transitionRoutine == null)
            {
                _transitionRoutine = StartCoroutine(LoadingSceneRoutine());
            }
            else
            {
                StopCoroutine(_transitionRoutine);
                _transitionRoutine = StartCoroutine(LoadingSceneRoutine());
            }
        }

        private string GetSceneToLoadName(SceneListEnum.LoadableScenes sceneToLoad)
        {
            _loadingSceneName = _sceneListSO._scenesRepository.Single(
                x => x._sceneType.Equals(sceneToLoad))._sceneName;

            return _loadingSceneName;
        }

        IEnumerator LoadingSceneRoutine()
        {

            ToggleEventSystem(false);

            ToggleCanvas(true);

            PlayTransitionAnimation(true);

            yield return new WaitUntil(() => !_transitionAnimationComponent.isPlaying);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_loadingSceneName, LoadSceneMode.Single);

            yield return new WaitUntil(()=> asyncLoad.isDone);

            PlayTransitionAnimation(false);

            yield return new WaitUntil(() => !_transitionAnimationComponent.isPlaying);

            ToggleEventSystem(true);

            ToggleCanvas(false);

        }

        private void PlayTransitionAnimation(bool state)
        {
            _transitionAnimationComponent.clip = state? _entranceAnimationClip: _exitAnimationClip;
            _transitionAnimationComponent.Play();
        }

        private void ToggleCanvas(bool state) => _transitionCanvas.enabled = state;

        private void ToggleEventSystem(bool state) => EventSystem.current.enabled = state;

        #endregion

    }
}
