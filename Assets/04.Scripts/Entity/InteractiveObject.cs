using MondayCatWorld.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MondayCatWorld
{
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
            LobbyUIPresenter.Instance.ActiveInteractionUI(transform);
        }

        private void Update()
        {
            if (!isPlayerNear) return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                LobbyUIPresenter.Instance.InActiveInteractionUI();
                OnInteract?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            isPlayerNear = false;

            if (!LobbyUIPresenter.Instance) return;
            LobbyUIPresenter.Instance.InActiveInteractionUI();
        }
    }
}