using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

public class DebuggingOnGUI : MonoBehaviour
{
    public void OnGUI()
    {
#if UNITY_EDITOR
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 30,
            fixedWidth = 300,
            fixedHeight = 80
        };
        
        if (GUILayout.Button("PlayerPrefs DeleteAll", myButtonStyle))
        {
            PlayerPrefs.DeleteAll();
        }
        
        if (GUILayout.Button("Add Point", myButtonStyle))
        {
            GameManager.Instance.AddPoint(100);
        }
#endif
    }
}
