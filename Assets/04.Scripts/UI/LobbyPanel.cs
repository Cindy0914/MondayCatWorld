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

        // 프로필 패널 초기화
        public void Init(Action action)
        {
            SetCharacterProfile();
            ProfileButton.onClick.AddListener(() => action());
            StartCoroutine(UpdateTime());
        }

        // 캐릭터 변경 시 프로필 이미지 변경
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
