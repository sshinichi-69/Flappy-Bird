using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.UI
{
    public class HighScorePanel : UiElement
    {
        [SerializeField] private NumberText text;

        private void OnEnable()
        {
            List<int> highScores = GameManager.Instance.HighScore.Scores;
            string textStr = "";
            for (int i = 0; i < highScores.Count; i++)
            {
                textStr += (i + 1).ToString() + "\t\t\t" + highScores[i].ToString();
                if (i < highScores.Count - 1)
                {
                    textStr += "\n";
                }
            }
            text.SetValue(textStr);
        }

        protected override void ResponsiveUi()
        {
            text.GetComponent<RectTransform>().anchoredPosition = Screen.height / 27f * Vector2.down;
            text.GetComponent<RectTransform>().sizeDelta = Screen.width / 2.5f * Vector2.one;
        }
    }
}
