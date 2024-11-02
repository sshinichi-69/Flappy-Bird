using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class Pipe : MonoBehaviour
    {
        private Vector2 size;

        private void Awake()
        {
            size = Tool.CalcWorldSize(GetComponent<SpriteRenderer>().sprite, transform.lossyScale);
        }

        public Rect GetRect()
        {
            return new Rect((Vector2)transform.position - size / 2, size);
        }
    }
}
