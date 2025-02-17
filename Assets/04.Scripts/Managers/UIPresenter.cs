using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

public class UIPresenter : Singleton<UIPresenter>
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] private RectTransform canvas;
    public RectTransform InteractionUI;

    public void ShowInteractionUI(Transform tr)
    {
        var offset = new Vector3(0f, 0f, 0);
        // var screenPos = uiCamera.WorldToScreenPoint(tr.position + offset);
        var mainCam = GameManager.Instance.MainCamera;
        var screenPos = mainCam.WorldToViewportPoint(tr.position + offset);

        // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPos, uiCamera, out var localPoint);

        InteractionUI.gameObject.SetActive(true);
        InteractionUI.anchorMin = screenPos;
        InteractionUI.anchorMax = screenPos;
        // InteractionUI.anchoredPosition = localPoint;
    }

    public void HideInteractionUI()
    {
        if (!InteractionUI.gameObject) return;
        InteractionUI.gameObject.SetActive(false);
    }
}