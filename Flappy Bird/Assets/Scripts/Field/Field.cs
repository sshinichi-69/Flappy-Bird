using UnityEngine;

namespace FlappyBird.InGame
{
    public class Field : MonoBehaviour
    {
        private readonly float top = 5f;
        private readonly float bottom = -3f;
        private readonly float left = -6f;
        private readonly float right = 6f;

        public float Top { get { return top; } }
        public float Bottom { get { return bottom; } }
        public float Left { get { return left; } }
        public float Right { get { return right; } }
    }
}
