using System;
using System.Collections;
using MondayCatWorld.SceneBase;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MondayCatWorld.Managers
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        private Define.Scene currentScene;
        private LoadingPanel loadingPanel;
        private Canvas canvas;
        
        private const float fakeMinDuration = 1f;
        private const float fakeMaxDuration = 2f;

        // 로딩에 필요한 
        public void Init()
        {
            switch (currentScene)
            {
                case Define.Scene.Title:
                    loadingPanel = TitleSceneBase.Instance.LoadingPanel;
                    canvas = TitleSceneBase.Instance.Canvas;
                    break;
                case Define.Scene.Lobby:
                    loadingPanel = LobbyUIPresenter.Instance.LoadingPanel;
                    canvas = LobbyUIPresenter.Instance.Canvas;
                    break;
                case Define.Scene.TheStack:
                    loadingPanel = StackUIPresenter.Instance.LoadingPanel;
                    canvas = StackUIPresenter.Instance.Canvas;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
                Debug.LogError("Canvas is not found");
            
            loadingPanel.gameObject.SetActive(false);
            loadingPanel.rectTr.offsetMin = Vector2.zero;
            loadingPanel.rectTr.offsetMax = Vector2.zero;
        }
        
        public void SetCurrentScene(Define.Scene scene)
        {
            currentScene = scene;
        }
        
        public void LoadSceneAsync(Define.Scene scene)
        {
            StartCoroutine(LoadSceneCoroutine(scene));
        }

        private IEnumerator LoadSceneCoroutine(Define.Scene scene)
        {
            loadingPanel.progressBar.value = 0f;
            loadingPanel.gameObject.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene.GetName());
            if (operation == null)
            {
                Debug.LogError("SceneLoader.LoadSceneAsync: operation is null");
                yield break;
            }
            operation.allowSceneActivation = false;
            
            float minDuration = UnityEngine.Random.Range(fakeMinDuration, fakeMaxDuration);
            float fakeLoadTime = 0f;

            while (!operation.isDone)
            {
                fakeLoadTime += Time.deltaTime;
                var fakeLoadRatio = fakeLoadTime / minDuration;
                
                var LoadRatio = Mathf.Min(operation.progress + 0.1f, fakeLoadRatio);
                loadingPanel.progressBar.value = LoadRatio;
                
                if (LoadRatio >= 1.0f)
                {
                    operation.allowSceneActivation = true;
                    break;
                }

                yield return null;
            }
        }
    }
}