using System;
using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using MondayCatWorld.Utils;
using TMPro;
using UnityEngine;

public class LobbyUIPresenter : SceneSingleton<LobbyUIPresenter>
{
    [SerializeField] private RectTransform InteractionUI;
    [SerializeField] private RectTransform NicknameUI;
    [SerializeField] private TextMeshProUGUI NicknameText;
    [SerializeField] private LobbyPanel lobbyPanel;
    [SerializeField] private ProfilePanel profilePanel;
    
    private readonly Vector3 interactOffset = new Vector3(0, 1f, 0);
    private readonly Vector3 playerOffset = new Vector3(0f, 80f, 0);
    
    private Transform PlayerTr = null;
    private Transform targetTr = null;
    private Camera mainCam = null;
    private bool isInteractable = false;

    public void Init()
    {
        mainCam = GameManager.Instance.MainCamera;
        var currentIndex = GameManager.Instance.ModelIndex;
        var modelCount = LobbySceneBase.Instance.PlayerModelData.ModelSprites.Count - 1;
        SetNicknameUI();
        lobbyPanel.Init(ActiveProfilePanel);
        profilePanel.Init(currentIndex, modelCount);
        profilePanel.Close();
    }

    private void SetNicknameUI()
    {
        PlayerTr = GameManager.Instance.Player.Tr;
        NicknameText.text = GameManager.Instance.Nickname;
        NicknameUI.gameObject.SetActive(true);
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
        var InteractionScreenPos = mainCam.WorldToViewportPoint(targetTr.position + interactOffset);
        InteractionUI.anchorMin = InteractionScreenPos;
        InteractionUI.anchorMax = InteractionScreenPos;
    }

    private void LateUpdate()
    {
        var playerScreenPos = mainCam.WorldToViewportPoint(PlayerTr.position);
        NicknameUI.anchorMin = playerScreenPos;
        NicknameUI.anchorMax = playerScreenPos;
        NicknameUI.anchoredPosition = playerOffset;
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