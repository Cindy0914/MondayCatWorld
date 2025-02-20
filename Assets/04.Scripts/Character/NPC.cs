using MondayCatWorld.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MondayCatWorld.Character
{
    // NPC 클래스
    // 에디터에서 대사를 입력받아 단순히 대사를 출력하는 역할
    // 대사 출력 후 구독중인 이벤트가 있다면 호출
    public class NPC : MonoBehaviour
    {
        [SerializeField] private Transform tr;
        [TextArea(5, 10)] public string dialogue;

        public UnityEvent onTalkEnd = new UnityEvent(); 
        private string[] dialogues;
        private int dialogueIndex = 0;
        private bool isTalking = false;

        private void Start()
        {
            dialogues = dialogue.Split('\n');
        }

        private void Update()
        {
            if (!isTalking) return;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetDialogueIndex();
                
                if (!isTalking) return;
                LobbyUIPresenter.Instance.SetSpeechBubbleText(dialogues[dialogueIndex]);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            var currentDialogue = dialogues[dialogueIndex];
            LobbyUIPresenter.Instance.ActiveSpeechBubble(currentDialogue, transform);
            isTalking = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            isTalking = false;
            dialogueIndex = 0;
            LobbyUIPresenter.Instance.InactiveSpeechBubble();
        }

        private void SetDialogueIndex()
        {
            dialogueIndex++;
            if (dialogueIndex >= dialogues.Length)
            {
                isTalking = false;
                dialogueIndex = 0;
                LobbyUIPresenter.Instance.InactiveSpeechBubble();
                onTalkEnd?.Invoke();
            }
        }
    }
}