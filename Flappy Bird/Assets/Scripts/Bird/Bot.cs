using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class Bot : Bird
    {
        private void Update()
        {
            if (GameManager.Instance.IsPlayingState())
            {
                EnterInput();
            }
        }

        private void EnterInput()
        {
            List<Rect> pipeObjectiveRects = PipeManager.Instance.GetFirstPipeObjectiveRect();
            if (pipeObjectiveRects != null && pipeObjectiveRects.Count == 2)
            {
                ActWithPipePair(pipeObjectiveRects);
            }
            else
            {
                ActWithNothing();
            }
        }

        private void ActWithPipePair(List<Rect> pipeObjectiveRects)
        {
            float yMinToAlive = (pipeObjectiveRects[0].yMin + pipeObjectiveRects[1].yMax) / 2 - jumpHeight / 2;
            if (transform.position.y <= yMinToAlive)
            {
                Jump();
            }
        }

        private void ActWithNothing()
        {
            if (transform.position.y < 0)
            {
                Jump();
            }
        }

        protected override void EndGame()
        {
            GameManager.Instance.BackToMenu();
        }
    }
}
