using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

public class TheStackSceneBase : SceneSingleton<TheStackSceneBase>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private LoadingPanel loadingPanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject cubePrefab;
    
    public Canvas Canvas => canvas;
    public Camera MainCamera => mainCamera;
    public LoadingPanel LoadingPanel => loadingPanel;

    private void Start()
    {
        SceneLoader.Instance.SetCurrentScene(Define.Scene.TheStack);
        SceneLoader.Instance.Init();
        PoolManager.Instance.CreatePool(cubePrefab, 10);
    }
}
