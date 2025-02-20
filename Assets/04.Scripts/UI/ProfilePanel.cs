using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
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
            var modelPrefab = LobbySceneBase.Instance.GetModelPrefab(modelIndex);
            GameManager.Instance.Player.SetModel(modelPrefab);
            GameManager.Instance.SetModelIndex(modelIndex);
            LobbyUIPresenter.Instance.ChangeLobbyPanelModel(); // 로비 패널의 프로필 이미지도 변경
            gameObject.SetActive(false);
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

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}