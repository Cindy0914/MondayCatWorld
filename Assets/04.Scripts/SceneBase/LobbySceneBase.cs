using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Character;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

public class LobbySceneBase : SceneSingleton<LobbySceneBase>
{
    // UI
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private LoadingPanel loadingPanel;
    
    // Object
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private InteractiveObject theStackCristal;

    public Camera MainCamera => mainCamera;
    public Canvas Canvas => canvas;
    public LoadingPanel LoadingPanel => loadingPanel;
    
    private void Start()
    {
        SetGameManager();
        SceneLoader.Instance.SetCurrentScene(Define.Scene.Lobby);
        SceneLoader.Instance.Init();
        theStackCristal.AddInteractEvent(TheStackCristalInteract);
    }

    private void SetGameManager()
    {
        var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.Tr.position = playerSpawnPoint.position;
        GameManager.Instance.SetPlayer(player);
        GameManager.Instance.SetCamera(mainCamera);
    }
    
    private void TheStackCristalInteract()
    {
        GameManager.Instance.LoadTheStackScene();
    }
}
