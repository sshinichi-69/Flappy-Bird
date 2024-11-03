using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class GameOverPanel : UiElement
    {
        [SerializeField] private NumberText scoreText;
        [SerializeField] private NumberText highScoreText;

        [SerializeField] private RawImage title;
        [SerializeField] private RawImage result;
        [SerializeField] private Button okBtn;

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
            GameManager.Instance.BackToMenu();
        }

        protected override void ResponsiveUi()
        {
            title.GetComponent<RectTransform>().anchoredPosition = Screen.height / 4f * Vector2.up;
            title.GetComponent<RectTransform>().localScale = Screen.width / 2f / title.texture.width * Vector3.one;

            result.GetComponent<RectTransform>().localScale = Screen.width / 1.33f / result.texture.width * Vector3.one;

            okBtn.GetComponent<RectTransform>().anchoredPosition = Screen.height / 4f * Vector2.down;
            okBtn.GetComponent<RectTransform>().localScale = Screen.width / 4f / okBtn.GetComponent<Image>().sprite.rect.width * Vector3.one;
        }
    }
}
