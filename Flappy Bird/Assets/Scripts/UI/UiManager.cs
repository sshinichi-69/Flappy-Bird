using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class UiManager : MonoBehaviour
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

        public void OnStartGame()
        {
            menu.gameObject.SetActive(false);
            play.OnStartGame();
        }

        public void OnPlayGame()
        {
            play.OnPlayGame();
        }

        public void OnEndGame()
        {
            int score = GameManager.Instance.Score;
            play.OnEndGame();
            gameOverPanel.gameObject.SetActive(true);
            gameOverPanel.SetScore(score);
            gameOverPanel.SetHighScore(GameManager.Instance.HighScore.GetHighestScore());
        }

        public void OnBackToMenu()
        {
            gameOverPanel.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }

        public void OnPlayDemo()
        {
            menu.gameObject.SetActive(false);
            play.OnPlayDemo();
        }

        public void OnEndDemo()
        {
            menu.gameObject.SetActive(true);
            play.OnEndDemo();
        }

        public void OnPauseGame()
        {
            play.OnPauseGame();
        }

        public void OnResumeGame()
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
