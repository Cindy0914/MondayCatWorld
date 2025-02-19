using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MondayCatWorld.UI
{
    public class SpeechBubblePanel : MonoBehaviour
    {
        public RectTransform BubbleRectTr;
        public RectTransform TextRectTr;
        public TextMeshProUGUI InputKeyText;
        public TextMeshProUGUI Text;

        private readonly Vector3 bubbleOffset = new(45f, 130f, 0);
        private const float duration = 1f;

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SetText(string text)
        {
            Text.text = text;
            SetBubbleSize();
            StartCoroutine(InputKeyAnim());
        }

        private void SetBubbleSize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(TextRectTr);

            var textSize = TextRectTr.sizeDelta;
            BubbleRectTr.sizeDelta = new Vector2(textSize.x + bubbleOffset.x, textSize.y + bubbleOffset.y);
        }

        private IEnumerator InputKeyAnim()
        {
            var elapsedTime = 0f;
            while (gameObject.activeSelf)
            {
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    InputKeyText.color = Color.Lerp(Color.white, Color.clear, elapsedTime / duration);
                    yield return null;
                }
                while (elapsedTime > 0)
                {
                    elapsedTime -= Time.deltaTime;
                    InputKeyText.color = Color.Lerp(Color.clear, Color.white, elapsedTime / duration);
                    yield return null;
                }
            }
        }
    }
}