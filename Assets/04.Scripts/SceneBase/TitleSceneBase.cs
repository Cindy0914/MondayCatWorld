using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

namespace MondayCatWorld.SceneBase
{
    // Title 씬에서 필요한 데이터를 가지고 있고, 씬 진입 시 초기화를 담당하는 클래스
    public class TitleSceneBase : SceneSingleton<TitleSceneBase>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private LoadingPanel loadingPanel;

        public Canvas Canvas => canvas;
        public LoadingPanel LoadingPanel => loadingPanel;

        private void Start()
        {
            SceneLoader.Instance.SetCurrentScene(Define.Scene.Title);
            SceneLoader.Instance.Init();
        }
    }
}