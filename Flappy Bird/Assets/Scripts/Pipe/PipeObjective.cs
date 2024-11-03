using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class PipeObjective : MonoBehaviour
    {
        [SerializeField] protected Pipe pipePrefab;

        protected List<Pipe> pipes;
        private float speed = 1f;

        protected virtual void Awake()
        {
            pipes = new List<Pipe>();
        }

        // Update is called once per frame
        void Update()
        {
            Translate();
        }

        public List<Rect> GetRects()
        {
            List<Rect> rects = new List<Rect>();
            foreach (Pipe pipe in pipes)
            {
                rects.Add(pipe.GetRect());
            }
            return rects;
        }

        public virtual float GetXCompletePoint()
        {
            return 0;
        }

        void Translate()
        {
            if (GameManager.Instance.IsPlayingState())
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
                if (transform.position.x < GameManager.Instance.Field.Left)
                {
                    PipeManager.Instance.DestroyPipeObjective(this);
                }
            }
        }
    }
}
