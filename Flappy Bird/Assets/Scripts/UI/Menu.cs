using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class Menu : UiElement
    {
        [SerializeField] private GameObject navigation;
        [SerializeField] private HighScorePanel highScore;
        [SerializeField] private Text demoText;

        [SerializeField] private RawImage title;
        [SerializeField] private Button startBtn;
        [SerializeField] private Button scoreBtn;

        private void Update()
        {
            if (Tool.IsScreenPressed())
            {
                BackToNavigation();
            }
        }

        public void OnStartBtnPressed()
        {
            GameManager.Instance.StartGame();
        }

        public void OnScoreBtnPressed()
        {
            navigation.SetActive(false);
            demoText.gameObject.SetActive(false);
            highScore.gameObject.SetActive(true);
        }

        public void BackToNavigation()
        {
            if (highScore.gameObject.activeSelf)
            {
                highScore.gameObject.SetActive(false);
                navigation.SetActive(true);
                demoText.gameObject.SetActive(true);
            }
        }

        public bool IsOnNavigationScreen()
        {
            return navigation.activeSelf;
        }

        protected override void ResponsiveUi()
        {
            title.GetComponent<RectTransform>().anchoredPosition = Screen.height / 4f * Vector2.up;
            title.GetComponent<RectTransform>().localScale = Screen.width / 2f / title.texture.width * Vector3.one;

            startBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 5f, -Screen.height / 4f);
            startBtn.GetComponent<RectTransform>().localScale = Screen.width / 4f / startBtn.GetComponent<Image>().sprite.rect.width * Vector3.one;

            scoreBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 5f, -Screen.height / 4f);
            scoreBtn.GetComponent<RectTransform>().localScale = Screen.width / 4f / startBtn.GetComponent<Image>().sprite.rect.width * Vector3.one;

            demoText.fontSize = (int)(Screen.width / 1080f * 64);
        }
    }
}
