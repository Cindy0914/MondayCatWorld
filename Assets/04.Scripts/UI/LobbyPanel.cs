using System;
using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
    public Image CharacterProfile;
    public Button ProfileButton;

    public void Init(Action action)
    {
        SetCharacterProfile();
        ProfileButton.onClick.AddListener(() => action());
    }
    
    public void SetCharacterProfile()
    {
        var index = GameManager.Instance.ModelIndex;
        var sprite = LobbySceneBase.Instance.GetProfileImage(index);
        CharacterProfile.sprite = sprite;
    }
}
