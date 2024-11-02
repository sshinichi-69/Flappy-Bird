using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FlappyBird.InGame
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private AudioClip dieSound;

        protected readonly float gravity = 9.8f;
        protected readonly float jumpHeight = 1f;
        protected float jumpVelocity;

        protected float verticalVelocity = 0f;
        protected bool isAlive = true;

        protected Vector2 size;
        protected Vector3 startPos;

        protected AudioSource audioSource;

        protected void Awake()
        {
            jumpVelocity = -Mathf.Sqrt(2 * gravity * jumpHeight);
            size = Tool.CalcWorldSize(GetComponent<SpriteRenderer>().sprite, transform.lossyScale);
            startPos = transform.position;

            audioSource = GetComponent<AudioSource>();
        }

        protected void FixedUpdate()
        {
            GameManager gm = GameManager.Instance;
            if (gm.IsPlayingState() || gm.GameState == GameState.ENDING)
            {
                Fall();
                if (gm.IsPlayingState())
                {
                    CheckCollision();
                }
            }
        }

        public virtual void OnPlayGame()
        {
            Jump();
        }

        public void OnEndGame()
        {
            GetComponent<Animator>().SetBool("IsFly", false);
        }

        public void OnBackToMenu()
        {
            isAlive = true;
            verticalVelocity = 0f;
            transform.position = startPos;
            GetComponent<Animator>().SetBool("IsFly", true);
            audioSource.clip = jumpSound;
        }

        public float GetXPos()
        {
            return transform.position.x - size.x / 2;
        }

        protected void Fall()
        {
            Field field = GameManager.Instance.Field;
            if (transform.position.y > field.Bottom)
            {
                transform.Translate(verticalVelocity * Time.fixedDeltaTime * Vector3.down);
                verticalVelocity += gravity * Time.deltaTime;
                if (transform.position.y > field.Top)
                {
                    EndGame();
                }
            }
            else
            {
                EndGame();
            }
        }

        protected void Jump()
        {
            verticalVelocity = jumpVelocity;
            audioSource.Play();
        }

        protected void CheckCollision()
        {
            Rect rect = new Rect((Vector2)transform.position - size / 2, size);
            if (PipeManager.Instance.IsCollide(rect))
            {
                EndGame();
            }
        }

        protected virtual void EndGame()
        {
            if (isAlive)
            {
                isAlive = false;
                GameManager.Instance.SwitchToEndingState();
                StartCoroutine(PlayDieSound());
            }
        }

        protected IEnumerator PlayDieSound()
        {
            audioSource.clip = hitSound;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.clip = dieSound;
            audioSource.Play();
        }
    }
}
