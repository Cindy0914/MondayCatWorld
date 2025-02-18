using System;
using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

public class LobbyUIPresenter : SceneSingleton<LobbyUIPresenter>
{
    [SerializeField] private RectTransform InteractionUI;
    [SerializeField] private LobbyPanel lobbyPanel;
    [SerializeField] private ProfilePanel profilePanel;
    
    private readonly Vector3 Offset = new Vector3(0, 1f, 0);
    
    private Transform targetTr = null;
    private Camera mainCam = null;
    private Vector3 screenPos = Vector3.zero;
    private bool isInteractable = false;

    public void Init()
    {
        mainCam = GameManager.Instance.MainCamera;
        var currentIndex = GameManager.Instance.ModelIndex;
        var modelCount = LobbySceneBase.Instance.PlayerModelData.ModelSprites.Count - 1;
        lobbyPanel.Init(ActiveProfilePanel);
        profilePanel.Init(currentIndex, modelCount);
        profilePanel.Close();
    }
    
    public void ShowInteractionUI(Transform target)
    {
        targetTr = target;    
        isInteractable = true;

        InteractionUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isInteractable) return;
        screenPos = mainCam.WorldToViewportPoint(targetTr.position + Offset);
        InteractionUI.anchorMin = screenPos;
        InteractionUI.anchorMax = screenPos;
    }
    
    public void HideInteractionUI()
    {
        if (!isInteractable) return;
        
        isInteractable = false;
        InteractionUI.gameObject.SetActive(false);
    }

    private void ActiveProfilePanel()
    {
        profilePanel.gameObject.SetActive(true);
    }

    public void ChangeModel()
    {
        lobbyPanel.SetCharacterProfile();
    }
}