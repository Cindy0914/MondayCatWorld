using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Games;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

public class TheStackSceneBase : SceneSingleton<TheStackSceneBase>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private LoadingPanel loadingPanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TheStack theStack;
    [SerializeField] private GameObject cubePrefab;
    
    public Canvas Canvas => canvas;
    public Camera MainCamera => mainCamera;
    public LoadingPanel LoadingPanel => loadingPanel;

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
