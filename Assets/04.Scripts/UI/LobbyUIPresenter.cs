using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

public class LobbyUIPresenter : SceneSingleton<LobbyUIPresenter>
{
    public RectTransform InteractionUI;
    
    private readonly Vector3 Offset = new Vector3(0, 0.5f, 0);
    
    public void ShowInteractionUI()
    {
        var mainCam = GameManager.Instance.MainCamera;
        var playerTr = GameManager.Instance.Player.Tr;
        var screenPos = mainCam.WorldToViewportPoint(playerTr.position + Offset);

        InteractionUI.gameObject.SetActive(true);
        InteractionUI.anchorMin = screenPos;
        InteractionUI.anchorMax = screenPos;
    }

    public void HideInteractionUI()
    {
        if (!InteractionUI.gameObject) return;
        InteractionUI.gameObject.SetActive(false);
    }
}