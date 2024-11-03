using UnityEngine;

namespace FlappyBird.UI
{
    public class UiManager : ElementManager
    {
        private static UiManager instance;

        [SerializeField] private Menu menu;
        [SerializeField] private PlayUi play;
        [SerializeField] private GameOverPanel gameOverPanel;

        private void Awake()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }
        }

        public override void OnStartGame()
        {
            menu.gameObject.SetActive(false);
            play.OnStartGame();
        }

        public override void OnPlayGame()
        {
            play.OnPlayGame();
        }

        public override void OnEndGame()
        {
            int score = GameManager.Instance.Score;
            play.OnEndGame();
            gameOverPanel.gameObject.SetActive(true);
            gameOverPanel.SetScore(score);
            gameOverPanel.SetHighScore(GameManager.Instance.HighScore.GetHighestScore());
        }

        public override void OnBackToMenu()
        {
            gameOverPanel.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }

        public override void OnPlayDemo()
        {
            menu.gameObject.SetActive(false);
            play.OnPlayDemo();
        }

        public override void OnEndDemo()
        {
            menu.gameObject.SetActive(true);
            play.OnEndDemo();
        }

        public override void OnPauseGame()
        {
            play.OnPauseGame();
        }

        public override void OnResumeGame()
        {
            play.OnResumeGame();
        }

        public void SetScore(int score)
        {
            play.SetScore(score);
        }

        public bool IsOnNavigationScreen()
        {
            return menu.IsOnNavigationScreen();
        }

        public static UiManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UiManager>();
                }
                return instance;
            }
        }
    }
}
