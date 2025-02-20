using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
using MondayCatWorld.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MondayCatWorld.UI
{
    public class ProfilePanel : MonoBehaviour
    {
        public Image ModelImage;
        public TextMeshProUGUI NicknameText;
        public Button LeftButton;
        public Button RightButton;
        public Button CloseButton;
        public Button ChangeButton;
        public TextMeshProUGUI pointText;

        private int modelIndex = 0;
        private int maxIndex = 0;

        public void Init(int currentIndex, int modelCount)
        {
            modelIndex = currentIndex;
            maxIndex = modelCount;
            var sprite = LobbySceneBase.Instance.GetProfileImage(modelIndex);
            SetModelImage(sprite);
            SetNicknameText();
            SetPoint();
            LeftButton.onClick.AddListener(OnLeftButtonClick);
            RightButton.onClick.AddListener(OnRightButtonClick);
            CloseButton.onClick.AddListener(Close);
            ChangeButton.onClick.AddListener(OnChangeButtonClick);
        }

        private void OnLeftButtonClick()
        {
            modelIndex--;
            if (modelIndex < 0)
                modelIndex = maxIndex;

            var sprite = LobbySceneBase.Instance.GetProfileImage(modelIndex);
            SetModelImage(sprite);
        }

        private void OnRightButtonClick()
        {
            modelIndex++;
            if (modelIndex > maxIndex)
                modelIndex = 0;

            var sprite = LobbySceneBase.Instance.GetProfileImage(modelIndex);
            SetModelImage(sprite);
        }

        private void OnChangeButtonClick()
        {
            // 게임매니저의 현재 모델 인덱스 변경
            GameManager.Instance.SetModelIndex(modelIndex);
            // 플레이어 모델 변경
            var modelPrefab = LobbySceneBase.Instance.GetModelPrefab(modelIndex);
            GameManager.Instance.Player.SetModel(modelPrefab);
            // 패널 비활성화
            gameObject.SetActive(false);
            // 로비 패널의 캐릭터 프로필 변경
            LobbyUIPresenter.Instance.ChangeLobbyPanelModel();
        }

        private void SetModelImage(Sprite sprite)
        {
            ModelImage.sprite = sprite;
        }

        private void SetNicknameText()
        {
            var nickname = GameManager.Instance.Nickname;
            NicknameText.text = nickname;
        }

        private void SetPoint()
        {
            var point = GameManager.Instance.Point;
            pointText.text = $"{point:000} P";
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}