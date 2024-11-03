using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class Fade : MonoBehaviour
    {
        private void OnEnable()
        {
            Color currentColor = GetComponent<RawImage>().color;
            GetComponent<RawImage>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        }

        public void Hide()
        {
            StartCoroutine(HideGradually());
        }

        IEnumerator HideGradually()
        {
            yield return new WaitForSeconds(0.1f);
            Color currentColor = GetComponent<RawImage>().color;
            if (currentColor.a > 0.1f)
            {
                GetComponent<RawImage>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - 0.1f);
                StartCoroutine(HideGradually());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
