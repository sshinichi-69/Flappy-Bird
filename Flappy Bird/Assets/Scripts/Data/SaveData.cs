using System.Collections.Generic;

namespace FlappyBird.Data
{
    [System.Serializable]
    public class SaveData
    {
        public int[] HighScores { get; private set; }

        public SaveData(bool isInitial = false)
        {
            int highScoresLength = 5;
            HighScores = new int[highScoresLength];
            if (isInitial)
            {
                for (int i = 0; i < highScoresLength; i++)
                {
                    HighScores[i] = 0;
                }
            }
            else
            {
                List<int> highScoreData = GameManager.Instance.HighScore.Scores;
                for (int i = 0; i < highScoresLength; i++)
                {
                    HighScores[i] = highScoreData[i];
                }
            }
        }
    }

}
