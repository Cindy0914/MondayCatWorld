using MondayCatWorld.Managers;
using MondayCatWorld.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MondayCatWorld.UI
{
    public class TitlePanel : MonoBehaviour
    {
        public TMP_InputField InputField;
        public GameObject WarningPanel;
        public TextMeshProUGUI WarningText;
        public Button ConfirmButton;
        public Button CloseButton;

        private const string NameEmpty = "이름을 입력해주세요.";
        private const string NameTooLong = "최대 8자까지 가능합니다.";
        private const int MaxNameLength = 8;

        private void Start()
        {
            ConfirmButton.onClick.AddListener(OnConfirmButtonClick);
            CloseButton.onClick.AddListener(() => WarningPanel.SetActive(false));

            string playerName = PlayerPrefs.GetString(Define.NameKey, string.Empty);
            if (!string.IsNullOrEmpty(playerName))
            {
                InputField.text = playerName;
            }
        }

        private void OnConfirmButtonClick()
        {
            string playerName = InputField.text;
            if (string.IsNullOrEmpty(playerName))
            {
                WarningText.text = NameEmpty;
                WarningPanel.SetActive(true);
                return;
            }

            if (playerName.Length > MaxNameLength)
            {
                WarningText.text = NameTooLong;
                WarningPanel.SetActive(true);
                return;
            }

            PlayerPrefs.SetString(Define.NameKey, playerName);
            GameManager.Instance.SetName(playerName);
            SceneLoader.Instance.LoadSceneAsync(Define.Scene.Lobby);
        }
    }
}