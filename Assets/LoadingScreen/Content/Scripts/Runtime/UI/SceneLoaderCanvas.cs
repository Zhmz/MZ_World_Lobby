using UnityEngine;

namespace Tuwan.SceneLoader
{
    public class SceneLoaderCanvas : UGuiForm
    {
        private static SceneLoaderCanvas ActiveSingleton = null;
        public bl_LoadingScreenUI loadingSceneUI = null;
        private bool isStartLoad = false;
        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            if (bl_SceneLoaderManager.IsGlobalLoadingScreen())
            {
                var sl = FindObjectsOfType<SceneLoaderCanvas>();
                if(ActiveSingleton == null)
                {
                    ActiveSingleton = this;
                    DontDestroyOnLoad(gameObject);
                }

                foreach (var loader in sl)
                {
                    loader.SetActive(loader == ActiveSingleton);
                }
            }
        }

        public void SetProgress(float progress)
        {
            loadingSceneUI.FilledImage.fillAmount = progress;
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            isStartLoad = true;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            isStartLoad = false;
        }
        float progress = 0.5f;
        float duration = 0.2f; // 模拟任务的总时长
        private void Update()
        {
            if (isStartLoad)
            {
                // 模拟进度增加
                progress += Time.deltaTime / duration;
                Debug.Log(progress);
                // 更新进度条或执行其他操作
                updateProgressUI(progress);
            }
        }

        private void updateProgressUI(float progress)
        {
            SetProgress(progress);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDestroy()
        {
            if (ActiveSingleton == this) ActiveSingleton = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}