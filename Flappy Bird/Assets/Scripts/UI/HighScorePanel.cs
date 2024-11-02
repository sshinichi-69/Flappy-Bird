using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class HighScorePanel : MonoBehaviour
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
    }
}
