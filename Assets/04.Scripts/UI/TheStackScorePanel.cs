using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheStackScorePanel : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MaxComboText;
    public TextMeshProUGUI ComboText;
    private readonly Color offsetColor = new Color(0.1f, 0.1f, 0.1f);

    public void Init()
    {
        ScoreText.text = "";
        MaxComboText.text = "";
        ComboText.gameObject.SetActive(false);
    }
    
    public void SetScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }
    
    public void SetMaxComboText(int maxCombo)
    {
        MaxComboText.text = $"MAX {maxCombo}";
    }
    
    public void SetComboText(int combo)
    {
        if (ComboText.gameObject.activeSelf == false)
            ComboText.gameObject.SetActive(true);
        
        ComboText.text = $"{combo} COMBO";
    }

    public void ChangeTextColor(Color color)
    {
        ScoreText.color = color + offsetColor;
        MaxComboText.color = color + offsetColor;
        ComboText.color = color + offsetColor;
    }
}