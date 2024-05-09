using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading;
using Codice.Client.BaseCommands;
using UnityEngine.EventSystems;

namespace Gazeus.DesafioMatch3
{
    public class TransitionController : Singleton<TransitionController>
    {

        #region Variables

        [SerializeField] private Canvas _transitionCanvas;
        [SerializeField] private CanvasGroup _transitionCanvasGroup;
        [SerializeField] private ScenesRepository _sceneListSO;

        private string _loadingSceneName;
        private Coroutine _transitionRoutine;
        private Tween _fadeTween;
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
        }

        private void OnDisable()
        {
            StartTransition -= StartSceneTransition;
        }

        #endregion

        #region Methods

        [ContextMenu("Teste")]
        private void TestUnitario()
        {
            StartSceneTransition(SceneListEnum.LoadableScenes.GAMEPLAY);
        }

        [ContextMenu("Voltar")]
        private void TestUnitario2()
        {
            StartSceneTransition(SceneListEnum.LoadableScenes.MENU);
        }

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

            FadeCanvas(true);

            yield return new WaitUntil(() => !_fadeTween.active);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_loadingSceneName, LoadSceneMode.Single);

            yield return new WaitUntil(()=> asyncLoad.isDone);

            FadeCanvas(false);
            ToggleEventSystem(true);
        }

        private void FadeCanvas(bool state)
        {
            _fadeTween = DOTween.To(() => _transitionCanvasGroup.alpha, x => _transitionCanvasGroup.alpha = x, state ? 1 : 0, 1f);
        }

        private void ToggleEventSystem(bool state) => EventSystem.current.enabled = state;


        #endregion

    }
}
