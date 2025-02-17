using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    private readonly UnityEvent OnInteract = new();
    private bool isPlayerNear = false;

    public void AddInteractEvent(UnityAction action)
    {
        OnInteract.AddListener(action);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = true;
        LobbyUIPresenter.Instance.ShowInteractionUI(transform);
    }

    private void Update()
    {
        if (!isPlayerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            LobbyUIPresenter.Instance.HideInteractionUI();
            OnInteract?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerNear = false;

        if (!LobbyUIPresenter.Instance) return;
        LobbyUIPresenter.Instance.HideInteractionUI();
    }
}