using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class PlayUi : MonoBehaviour
    {
        [SerializeField] private RawImage startImg;
        [SerializeField] private NumberText scoreText;
        [SerializeField] private Button pauseBtn;

        public void OnStartGame()
        {
            SetScore(0);
            startImg.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            pauseBtn.gameObject.SetActive(true);
        }

        public void OnPlayGame()
        {
            startImg.GetComponent<Fade>().Hide();
        }

        public void OnEndGame()
        {
            scoreText.gameObject.SetActive(false);
            pauseBtn.gameObject.SetActive(false);
        }
        public void OnPlayDemo()
        {
            scoreText.gameObject.SetActive(true);
            scoreText.SetValue(0);
        }

        public void OnEndDemo()
        {
            scoreText.gameObject.SetActive(false);
        }

        public void OnPauseGame()
        {
            pauseBtn.gameObject.SetActive(false);
        }

        public void OnResumeGame()
        {
            pauseBtn.gameObject.SetActive(true);
        }

        public void SetScore(int score)
        {
            scoreText.SetValue(score);
        }

        public void Pause()
        {
            GameManager.Instance.SwitchToPausingState();
        }
    }
}
