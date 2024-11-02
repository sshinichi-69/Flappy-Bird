using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class PipePair : PipeObjective
    {
        void Start()
        {
            float topPipeDistance = 2f;
            float spaceZeroPipeDistance = 8f;
            float yHighestUpperPipePos = 9f;
            float maxPipeDistanceInPair = GameManager.Instance.Field.Top - GameManager.Instance.Field.Bottom;

            float yUpperPipePos = Random.Range(yHighestUpperPipePos - maxPipeDistanceInPair + topPipeDistance, yHighestUpperPipePos);
            GameObject upperPipe = Instantiate(pipePrefab.gameObject, new Vector3(transform.position.x, yUpperPipePos, 0), Quaternion.identity);
            upperPipe.transform.SetParent(transform);
            upperPipe.GetComponent<SpriteRenderer>().flipY = true;
            pipes.Add(upperPipe.GetComponent<Pipe>());

            float yLowerPipePos = yUpperPipePos - spaceZeroPipeDistance - topPipeDistance;
            GameObject lowerPipe = Instantiate(pipePrefab.gameObject, new Vector3(transform.position.x, yLowerPipePos, 0), Quaternion.identity);
            lowerPipe.transform.SetParent(transform);
            pipes.Add(lowerPipe.GetComponent<Pipe>());
        }

        public override float GetXCompletePoint()
        {
            if (pipes.Count > 0)
            {
                return pipes[0].GetRect().xMax;
            }
            return GameManager.Instance.Field.Right;
        }
    }
}
