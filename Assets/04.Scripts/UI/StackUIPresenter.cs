using System.Collections;
using System.Collections.Generic;
using MondayCatWorld.Managers;
using UnityEngine;

public class StackUIPresenter : SceneSingleton<StackUIPresenter>
{
    [SerializeField] private TheStackMenuPanel menuPanel;
    [SerializeField] private TheStackScorePanel scorePanel;

    public void Init()
    {
        MenuPanelInit();
        scorePanel.Init();
    }

    private void MenuPanelInit()
    {
        menuPanel.StartButton.onClick.AddListener(StartGame);
        menuPanel.RetryButton.onClick.AddListener(RetryButton);
        menuPanel.ExitButton.onClick.AddListener(() =>
        {
            PoolManager.Instance.LoadSceneClearPool();
            GameManager.Instance.LoadLobbyScene();
        });
    }
    
    private void RetryButton()
    {
        menuPanel.gameObject.SetActive(false);
        scorePanel.SetScoreText(0);
        scorePanel.SetMaxComboText(0);
        TheStackSceneBase.Instance.RetryGame();
    }
    
    // 게임 시작 시 현재 게임의 점수와 콤보를 보여주기 위해 초기화
    private void StartGame()
    {
        menuPanel.gameObject.SetActive(false);
        scorePanel.SetScoreText(0);
        scorePanel.SetMaxComboText(0);
        TheStackSceneBase.Instance.StartGame();
    }

    // 씬 진입 시 보여 줄 최고 점수와 최고 콤보
    public void InitBestScore(int bestScore, int bestCombo)
    {
        if (bestScore != 0)
            scorePanel.SetScoreText(bestScore);
        
        if (bestCombo != 0)
            scorePanel.SetMaxComboText(bestCombo);
    }
    
    public void UpdateCurrentCombo(int combo)
    {
        scorePanel.SetComboText(combo);
    }

    public void ResetCombo()
    {
        scorePanel.ComboText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scorePanel.SetScoreText(score);
    }
    
    public void UpdateMaxCombo(int maxCombo)
    {
        scorePanel.SetMaxComboText(maxCombo);
    }
    
    public void ChangeTextColor(Color color)
    {
        scorePanel.ChangeTextColor(color);
    }

    public void GameOver()
    {
        menuPanel.gameObject.SetActive(true);
        menuPanel.StartButton.gameObject.SetActive(false);
        menuPanel.RetryButton.gameObject.SetActive(true);
    }
}
