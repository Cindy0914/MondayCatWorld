using MondayCatWorld;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

namespace MondayCatWorld.SceneBase
{
    // TheStack 씬에서 필요한 데이터를 가지고 있고, 씬 진입 시 초기화를 담당하는 클래스
    public class TheStackSceneBase : SceneSingleton<TheStackSceneBase>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private TheStack theStack;
        [SerializeField] private GameObject cubePrefab;

        public Camera MainCamera => mainCamera;

        private void Start()
        {
            SceneLoader.Instance.SetCurrentScene(Define.Scene.TheStack);
            SceneLoader.Instance.Init();
            StackUIPresenter.Instance.Init();
            PoolManager.Instance.CreatePool(cubePrefab, 10);
        }

        public void StartGame()
        {
            theStack.StartGame();
        }

        public void RetryGame()
        {
            theStack.Retry();
            theStack.StartGame();
        }
    }
}