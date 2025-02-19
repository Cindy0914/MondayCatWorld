using System;
using System.Collections;
using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MondayCatWorld.UI
{
    public class LobbyPanel : MonoBehaviour
    {
        public Image CharacterProfile;
        public Button ProfileButton;
        public TextMeshProUGUI TimeText;
        
        private readonly WaitForSeconds wait = new(1f);

        public void Init(Action action)
        {
            SetCharacterProfile();
            ProfileButton.onClick.AddListener(() => action());
            StartCoroutine(UpdateTime());
        }

        public void SetCharacterProfile()
        {
            var index = GameManager.Instance.ModelIndex;
            var sprite = LobbySceneBase.Instance.GetProfileImage(index);
            CharacterProfile.sprite = sprite;
        }

        private IEnumerator UpdateTime()
        {
            while (true)
            {
                TimeText.text = DateTime.Now.ToString("yyyy-M-d (ddd)\ntt hh시 mm분");
                yield return wait;
            }
        }
    }
}
