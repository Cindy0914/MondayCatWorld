using System;
using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
    public Image CharacterProfile;
    public Button ProfileButton;
    public TextMeshProUGUI TimeText;
    private WaitForSeconds wait = new WaitForSeconds(1f);

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
