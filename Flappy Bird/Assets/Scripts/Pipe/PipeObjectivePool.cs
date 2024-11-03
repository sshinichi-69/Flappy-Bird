using UnityEngine;
using UnityEngine.Pool;

namespace FlappyBird.InGame
{
    public class PipeObjectivePool : MonoBehaviour
    {
        [SerializeField] private PipePair pipePairPrefab;

        private ObjectPool<PipePair> pipePairPool;

        private void Awake()
        {
            pipePairPool = new ObjectPool<PipePair>(CreatePipePair, OnGetFromPool, OnReleaseFromPool, null, true, 3);
        }

        public PipePair InstantiatePipePair()
        {
            return pipePairPool.Get();
        }

        public void DestroyPipe(PipeObjective pipeObjective)
        {
            if (pipeObjective is PipePair)
            {
                pipePairPool.Release(pipeObjective as PipePair);
            }
        }

        PipePair CreatePipePair()
        {
            GameObject pipePairObj = Instantiate(pipePairPrefab.gameObject);
            pipePairObj.transform.SetParent(transform);
            return pipePairObj.GetComponent<PipePair>();
        }

        void OnGetFromPool(PipeObjective pipe)
        {
            pipe.gameObject.SetActive(true);
        }

        void OnReleaseFromPool(PipeObjective pipe)
        {
            pipe.transform.SetParent(transform);
            pipe.transform.position = GameManager.Instance.Field.Right * Vector3.right;
            pipe.gameObject.SetActive(false);
        }
    }
}
