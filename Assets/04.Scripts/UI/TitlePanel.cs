using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using MondayCatWorld.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    public TMP_InputField InputField;
    public GameObject WarningPanel;
    public TextMeshProUGUI WarningText;
    public Button ConfirmButton;
    public Button CloseButton;
    
    private const string NameEmpty = "이름을 입력해주세요.";
    private const string NameTooLong = "최대 6자까지 가능합니다.";
    private const int MAX_NAME_LENGTH = 6;
    
    private void Start()
    {
        ConfirmButton.onClick.AddListener(OnConfirmButtonClick);
        CloseButton.onClick.AddListener(() => WarningPanel.SetActive(false));
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
        
        if (playerName.Length > MAX_NAME_LENGTH)
        {
            WarningText.text = NameTooLong;
            WarningPanel.SetActive(true);
            return;
        }
        
        GameManager.Instance.SetName(playerName);
        SceneLoader.Instance.LoadSceneAsync(Define.Scene.Lobby);
    }
}
