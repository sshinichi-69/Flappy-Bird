using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.UI {
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private NumberText scoreText;
        [SerializeField] private NumberText highScoreText;

        public void SetScore(int score)
        {
            scoreText.SetValue(score);
        }

        public void SetHighScore(int highScore)
        {
            highScoreText.SetValue(highScore);
        }

        public void RestartGame()
        {
            GameManager.Instance.SwitchToMenuState();
        }
    }
}
