using System;
using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MondayCatWorld.Managers
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        private Define.Scene currentScene;
        private LoadingPanel loadingPanel;
        private Canvas canvas;
        
        private const float fakeMinDuration = 1f;
        private const float fakeMaxDuration = 2f;

        public void Init()
        {
            switch (currentScene)
            {
                case Define.Scene.Title:
                    loadingPanel = TitleSceneBase.Instance.LoadingPanel;
                    canvas = TitleSceneBase.Instance.Canvas;
                    break;
                case Define.Scene.Lobby:
                    loadingPanel = LobbySceneBase.Instance.LoadingPanel;
                    canvas = LobbySceneBase.Instance.Canvas;
                    break;
                case Define.Scene.TheStack:
                    loadingPanel = TheStackSceneBase.Instance.LoadingPanel;
                    canvas = TheStackSceneBase.Instance.Canvas;
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
            operation.allowSceneActivation = false;
            
            float minDuration = UnityEngine.Random.Range(fakeMinDuration, fakeMaxDuration);
            float fakeLoadTime = 0f;
            float fakeLoadRatio = 0f;

            while (!operation.isDone)
            {
                fakeLoadTime += Time.deltaTime;
                fakeLoadRatio = fakeLoadTime / minDuration;
                
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