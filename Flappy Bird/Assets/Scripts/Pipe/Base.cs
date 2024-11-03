using UnityEngine;

namespace FlappyBird.InGame
{
    public class Base : MonoBehaviour
    {
        private float speed = 1f;
        private Vector3 originPos;

        private void Awake()
        {
            originPos = transform.position;
        }

        private void Update()
        {
            if (GameManager.Instance.IsPlayingState())
            {
                transform.Translate(speed * Time.deltaTime * Vector3.left);
                if (transform.position.x < originPos.x - 1)
                {
                    transform.position = originPos;
                }
            }
        }
    }
}
