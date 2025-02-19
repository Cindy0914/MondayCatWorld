using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MondayCatWorld.UI;
using UnityEngine;

namespace MondayCatWorld.Character
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private Transform tr;
        [TextArea(5, 10)] public string dialogue;

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
            LobbyUIPresenter.Instance.HideSpeechBubble();
        }

        private void SetDialogueIndex()
        {
            dialogueIndex++;
            if (dialogueIndex >= dialogues.Length)
            {
                isTalking = false;
                dialogueIndex = 0;
                LobbyUIPresenter.Instance.HideSpeechBubble();
            }
        }
    }
}