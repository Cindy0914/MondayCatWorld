using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private UnityEvent OnInteract;
    private bool isPlayerNear = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (!other.CompareTag("Player")) return;
        
        isPlayerNear = true;
        UIPresenter.Instance.ShowInteractionUI(tr);
    }

    private void Update()
    {
        if (!isPlayerNear) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        isPlayerNear = false;
        UIPresenter.Instance.HideInteractionUI();
    }
}
