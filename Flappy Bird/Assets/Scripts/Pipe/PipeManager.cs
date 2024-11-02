using FlappyBird.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class PipeManager : MonoBehaviour
    {
        private static PipeManager instance;

        [SerializeField] private PipeObjectivePool pipeObjectivePool;
        [SerializeField] private GameObject pipeContainer;

        private float xPipeInitPos;

        private List<PipeObjective> pipeObjectives;

        private void Awake()
        {
            pipeObjectives = new List<PipeObjective>();
            xPipeInitPos = GameManager.Instance.Field.Right;

            if (instance == null)
            {
                instance = FindObjectOfType<PipeManager>();
            }
        }

        public bool IsCollide(Rect rect)
        {
            foreach (PipeObjective pipeObjective in pipeObjectives)
            {
                List<Rect> pipeRects = pipeObjective.GetRects();
                foreach (Rect pipeRect in pipeRects) {
                    if (CustomPhysics.IsRectangleIntersect(pipeRect, rect))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Rect> GetFirstPipeObjectiveRect()
        {
            if (pipeObjectives.Count > 0)
            {
                return pipeObjectives[0].GetRects();
            }
            return null;
        }

        public float GetXFirstPipeObjectivePos()
        {
            if (pipeObjectives.Count > 0) {
                return pipeObjectives[0].GetXCompletePoint();
            }
            return GameManager.Instance.Field.Right;
        }

        public void RemovePipeObjective(int idx = 0)
        {
            pipeObjectives.RemoveAt(idx);
        }

        public void OnPlayGame()
        {
            GeneratePipe();
        }

        public void OnBackToMenu()
        {
            pipeObjectives.Clear();
            for (int i = pipeContainer.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = pipeContainer.transform.GetChild(i);
                PipeObjective pipeObjective = child.GetComponent<PipeObjective>();
                if (pipeObjective == null)
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    DestroyPipeObjective(pipeObjective);
                }
            }
        }

        public async void GeneratePipe()
        {
            if (GameManager.Instance.IsPlayingState())
            {
                PipePair pipePair = pipeObjectivePool.InstantiatePipePair();
                pipePair.transform.position = new Vector3(xPipeInitPos, 0, 0);
                pipePair.transform.SetParent(pipeContainer.transform);
                pipeObjectives.Add(pipePair.GetComponent<PipePair>());
                await Tool.Timer(
                    GeneratePipe,
                    () => GameManager.Instance.IsPlayingState(),
                    4f,
                    () => GameManager.Instance.GameState == GameState.PAUSING);
            }
        }

        public void DestroyPipeObjective(PipeObjective pipe)
        {
            pipeObjectivePool.DestroyPipe(pipe);
        }

        public static PipeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PipeManager>();
                }
                return instance;
            }
        }
    }
}
