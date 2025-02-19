using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
using TMPro;
using UnityEngine;

namespace MondayCatWorld.UI
{
    public class LobbyUIPresenter : SceneSingleton<LobbyUIPresenter>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private LoadingPanel loadingPanel;

        // world space UI
        [SerializeField] private RectTransform InteractionUI;
        [SerializeField] private RectTransform NicknameUI;
        [SerializeField] private TextMeshProUGUI NicknameText;
        [SerializeField] private SpeechBubblePanel speechBubblePanel;

        // screen space UI
        [SerializeField] private LobbyPanel lobbyPanel;
        [SerializeField] private ProfilePanel profilePanel;
        [SerializeField] private TextMeshProUGUI testText;

        private readonly Vector3 interactOffset = new Vector3(90, 75f, 0);
        private readonly Vector3 bubbleOffset = new Vector3(20f, 140f, 0);
        private readonly Vector3 playerOffset = new Vector3(0f, -103f, 0);

        private Camera mainCam = null;
        private Transform PlayerTr = null;
        private Transform targetTr = null;
        private Transform npcTr = null;
        private bool isInteractable = false;
        private bool isTalking = false;

        public Canvas Canvas => canvas;
        public LoadingPanel LoadingPanel => loadingPanel;

        public void Init()
        {
            mainCam = GameManager.Instance.MainCamera;
            SetNicknameUI();
            var currentIndex = GameManager.Instance.ModelIndex;
            var modelCount = LobbySceneBase.Instance.PlayerModelData.ModelSprites.Count - 1;
            profilePanel.Init(currentIndex, modelCount);
            profilePanel.Close();
            lobbyPanel.Init(ActiveProfilePanel);
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
            if (isInteractable)
            {
                var InteractionScreenPos = mainCam.WorldToViewportPoint(targetTr.position);
                InteractionUI.anchorMin = InteractionScreenPos;
                InteractionUI.anchorMax = InteractionScreenPos;
                InteractionUI.anchoredPosition = interactOffset;
            }

            if (isTalking)
            {
                var npcScreenPos = mainCam.WorldToViewportPoint(npcTr.position);
                speechBubblePanel.BubbleRectTr.anchorMin = npcScreenPos;
                speechBubblePanel.BubbleRectTr.anchorMax = npcScreenPos;
                speechBubblePanel.BubbleRectTr.anchoredPosition = bubbleOffset;
            }
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

        public void ActiveSpeechBubble(string text, Transform tr)
        {
            speechBubblePanel.gameObject.SetActive(true);
            npcTr = tr;
            isTalking = true;
            SetSpeechBubbleText(text);
        }

        public void SetSpeechBubbleText(string text)
        {
            speechBubblePanel.SetText(text);
        }

        public void HideSpeechBubble()
        {
            isTalking = false;
            speechBubblePanel.gameObject.SetActive(false);
        }
    }
}