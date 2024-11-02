using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.Data
{
    public class HighScore
    {
        public List<int> Scores { get; private set; }

        public HighScore()
        {
            int scoresLength = 5;
            Scores = new List<int>();
            for (int i = 0; i < scoresLength; i++)
            {
                Scores.Add(0);
            }
        }

        public HighScore(int[] highScores)
        {
            int scoresLength = 5;
            Scores = new List<int>();
            if (highScores != null && highScores.Length == scoresLength)
            {
                for (int i = 0; i < scoresLength; i++)
                {
                    Scores.Add(highScores[i]);
                }
            }
            else
            {
                for (int i = 0; i < scoresLength; i++)
                {
                    Scores.Add(0);
                }
            }
        }

        public void SetNewScore(int score)
        {
            for (int i = 0; i < Scores.Count; i++)
            {
                if (score > Scores[i])
                {
                    Scores.Insert(i, score);
                    Scores.RemoveAt(Scores.Count - 1);
                    break;
                }
            }
        }

        public int GetHighestScore()
        {
            return Scores[0];
        }
    }
}
