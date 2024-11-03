using UnityEngine;

namespace FlappyBird.InGame
{
    public class Player : Bird
    {
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Instance.IsPlayingState())
            {
                if (Input.touchCount == 1)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        Jump();
                    }
                }
            }
        }
    }
}
