using System;
using System.Collections;
using Managers;
using SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using EventHandler = Utils.EventHandler;

namespace Teleport
{
    public class TeleportManager : Singleton<TeleportManager>, ISaveable
    {
        /// 初始场景
        public string initialScene;

        /// 淡入淡出的画布组件
        public CanvasGroup fadeCanvasGroup;
        /// 淡入淡出的动画时间
        public float fadeDuration = 1f;

        /// 是否正在切换场景
        private bool _isTransitioning;

        /// 是否可以切换场景
        private bool _canTransition = true;

        private void Start()
        {
            // StateSaveManager.activatableStates["Teleport from H2 to H3"] = false;
            // StartCoroutine(TransitionCoroutine(null, initialScene));
            ISaveable saveable = this;
            saveable.SaveableRegister();
        }

        private void OnEnable()
        {
            EventHandler.GameStateChangeEvent += OnGameStateChange;
            EventHandler.StartNewGameEvent += OnStartNewGameEvent;
        }

        private void OnDisable()
        {
            EventHandler.GameStateChangeEvent -= OnGameStateChange;
            EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
        }

        private void OnStartNewGameEvent(int obj)
        {
            StartCoroutine(TransitionCoroutine("Menu", initialScene));
        }

        private void OnGameStateChange(GameState gameState)
        {
            _canTransition = gameState == GameState.Gameplay;
        }

        /// 场景转换
        public void Transition(string fromScene, string toScene)
        {
            if (!_isTransitioning && _canTransition)
                StartCoroutine(TransitionCoroutine(fromScene, toScene));
        }

        /// 携程切换场景
        private IEnumerator TransitionCoroutine(string fromScene, string toScene)
        {
            // 设置正在切换场景
            _isTransitioning = true;
            fadeCanvasGroup.blocksRaycasts = true; // 遮挡点击
            // 开始淡出
            yield return FadeCoroutine(1);

            if (!string.IsNullOrEmpty(fromScene))
            {
                // 设置卸载场景前事件
                EventHandler.CallBeforeSceneUnloadEvent();
                // 异步卸载当前场景，并等待卸载完成
                yield return SceneManager.UnloadSceneAsync(fromScene);
            }

            // 异步加载目标场景，并等待加载完成
            yield return SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
            // 场景加载完成后，激活目标场景
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(toScene));
            // 设置加载场景后事件
            EventHandler.CallAfterSceneLoadEvent();

            // 结束淡出
            yield return FadeCoroutine(0);
            // 设置切换场景完成
            fadeCanvasGroup.blocksRaycasts = false; // 解除遮挡点击
            _isTransitioning = false;
        }

        /// 淡入淡出携程
        private IEnumerator FadeCoroutine(float targetAlpha)
        {
            var fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
            {
                fadeCanvasGroup.alpha =
                    Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public GameSaveData GenerateSaveData()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            return new GameSaveData
            {
                currentScene = currentScene
            };
        }

        public void RestoreSaveData(GameSaveData saveData)
        {
            StartCoroutine(TransitionCoroutine("Menu", saveData.currentScene));
        }
    }
}