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

    // Data
    [SerializeField] private ModelData playerModelData;
    
    public Camera MainCamera => mainCamera;
    public Canvas Canvas => canvas;
    public LoadingPanel LoadingPanel => loadingPanel;
    public ModelData PlayerModelData => playerModelData;
    
    private void Start()
    {
        SetGameManager();
        SceneLoader.Instance.SetCurrentScene(Define.Scene.Lobby);
        SceneLoader.Instance.Init();
        LobbyUIPresenter.Instance.Init();
        theStackCristal.AddInteractEvent(TheStackCristalInteract);
    }

    private void SetGameManager()
    {
        var modelNum = PlayerPrefs.GetInt(Define.ModelNumKey, 5);
        var model = Instantiate(playerModelData.ModelPrefabs[modelNum]);
        
        var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.Tr.position = playerSpawnPoint.position;
        player.SetModel(model);
        
        GameManager.Instance.SetPlayer(player);
        GameManager.Instance.SetModelIndex(modelNum);
        GameManager.Instance.SetCamera(mainCamera);
    }
    
    private void TheStackCristalInteract()
    {
        GameManager.Instance.LoadTheStackScene();
    }
    
    public GameObject GetModelPrefab(int modelNum)
    {
        return playerModelData.ModelPrefabs[modelNum];
    }
    
    public Sprite GetProfileImage(int modelNum)
    {
        return playerModelData.ModelSprites[modelNum];
    }
}
