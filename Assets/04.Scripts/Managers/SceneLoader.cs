using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MondayCatWorld.Managers
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public GameObject loadingUi;
        public Slider progressBar;
        private readonly WaitForSeconds waitForSeconds = new(0.5f);
        
        public void LoadSceneAsync(Define.Scene scene)
        {
            StartCoroutine(LoadSceneCoroutine(scene));
        }

        private IEnumerator LoadSceneCoroutine(Define.Scene scene)
        {
            loadingUi.SetActive(true);
            progressBar.value = 0.1f;
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene.GetName());

            yield return waitForSeconds;
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;
                yield return null;
            }
        }
    }
}