using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

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
