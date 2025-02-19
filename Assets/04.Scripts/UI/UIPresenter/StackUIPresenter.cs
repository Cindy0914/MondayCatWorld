using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
using UnityEngine;

namespace MondayCatWorld.UI
{
    public class StackUIPresenter : SceneSingleton<StackUIPresenter>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private LoadingPanel loadingPanel;
        [SerializeField] private TheStackMenuPanel menuPanel;
        [SerializeField] private TheStackScorePanel scorePanel;
        [SerializeField] private GameObject PlayGuideUI;

        public Canvas Canvas => canvas;
        public LoadingPanel LoadingPanel => loadingPanel;
        
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
            PlayGuideUI.SetActive(true);
            scorePanel.SetScoreText(0);
            scorePanel.SetMaxComboText(0);
            TheStackSceneBase.Instance.RetryGame();
        }

        // 게임 시작 시 현재 게임의 점수와 콤보를 보여주기 위해 초기화
        private void StartGame()
        {
            menuPanel.gameObject.SetActive(false);
            PlayGuideUI.SetActive(true);
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

        public bool IsGuidePanelActive()
        {
            return PlayGuideUI.activeSelf;
        }

        public void HideGuidePanel()
        {
            PlayGuideUI.SetActive(false);
        }

        public void GameOver()
        {
            menuPanel.gameObject.SetActive(true);
            menuPanel.StartButton.gameObject.SetActive(false);
            menuPanel.RetryButton.gameObject.SetActive(true);
        }
    }
}