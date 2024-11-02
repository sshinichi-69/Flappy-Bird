using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class Bot : Bird
    {
        public override void OnPlayGame()
        {
            base.OnPlayGame();
            StartCoroutine(EnterInput());
        }

        private IEnumerator EnterInput()
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
            yield return new WaitForSeconds(1 / 30f);
            if (GameManager.Instance.IsPlayingState())
            {
                StartCoroutine(EnterInput());
            }
        }

        private void ActWithPipePair(List<Rect> pipeObjectiveRects)
        {
            float yMinToAlive = (pipeObjectiveRects[0].yMin + pipeObjectiveRects[1].yMax) / 2 - jumpHeight / 2;
            //Debug.Log($"{transform.position.y} <= {yMinToAlive} <---> {pipeObjectiveRects[1].yMax}");
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
            GameManager.Instance.SwitchToMenuState();
        }
    }
}
