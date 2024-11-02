using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird.UI
{
    public class NumberText : MonoBehaviour
    {
        [SerializeField] TextAnchor alignment = TextAnchor.MiddleCenter;

        [SerializeField] List<RawImage> digitImgPrefabs;
        [SerializeField] RectTransform text;

        Vector2 digitImgSize;

        List<RawImage> digitImgs;

        public void SetValue(int number)
        {
            SetValue(number.ToString());
        }

        public void SetValue(string str)
        {
            Clear();
            Vector2 writingPos = new Vector2(digitImgSize.x, -digitImgSize.y) / 2;
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    RawImage digitImg = InstantiateDigitImg(c - '0', writingPos);
                    digitImgs.Add(digitImg);
                    writingPos += digitImgSize.x * Vector2.right;
                }
                else if (c == '\n')
                {
                    writingPos = new Vector2(digitImgSize.x / 2, writingPos.y - digitImgSize.y * 1.25f);
                }
                else
                {
                    writingPos += digitImgSize.x * Vector2.right;
                }
            }
            Align();
        }

        private void Clear()
        {
            if (digitImgs == null)
            {
                Init();
            }
            else {
                for (int i = digitImgs.Count - 1; i >= 0; i--)
                {
                    RawImage digit = digitImgs[i];
                    digitImgs.RemoveAt(i);
                    Destroy(digit.gameObject);
                }
            }
        }

        private void Init()
        {
            digitImgs = new List<RawImage>();

            RawImage digitImgSample = digitImgPrefabs[0];
            Texture textureSample = digitImgSample.GetComponent<RawImage>().texture;
            Vector2 scale = digitImgSample.GetComponent<RectTransform>().lossyScale;
            digitImgSize = new Vector2(textureSample.width * scale.x, textureSample.height * scale.y);
        }

        private RawImage InstantiateDigitImg(int digit, Vector2 pos)
        {
            RawImage digitImg = digitImgPrefabs[0];
            if (digit >= 0 && digit < digitImgPrefabs.Count)
            {
                digitImg = digitImgPrefabs[digit];
            }
            GameObject digitObj = Instantiate(digitImg.gameObject);
            digitObj.GetComponent<RectTransform>().anchoredPosition = pos;
            digitObj.transform.SetParent(text.transform, false);
            return digitObj.GetComponent<RawImage>();
        }

        private void Align()
        {
            if (digitImgs.Count == 0)
            {
                return;
            }
            
            Vector2 halfSize = GetComponent<RectTransform>().sizeDelta / 2;
            Vector2 halfTextSize = GetTextSize() / 2;
            
            switch (alignment)
            {
                case TextAnchor.UpperLeft:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfSize.x, halfSize.y);
                    break;
                case TextAnchor.UpperCenter:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfTextSize.x, halfSize.y);
                    break;
                case TextAnchor.UpperRight:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(halfSize.x - halfTextSize.x * 2, halfSize.y);
                    break;
                case TextAnchor.MiddleLeft:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfSize.x, halfTextSize.y);
                    break;
                case TextAnchor.MiddleCenter:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfTextSize.x, halfTextSize.y);
                    break;
                case TextAnchor.MiddleRight:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(halfSize.x - halfTextSize.x * 2, halfTextSize.y);
                    break;
                case TextAnchor.LowerLeft:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfSize.x, halfTextSize.y * 2 - halfSize.y);
                    break;
                case TextAnchor.LowerCenter:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(-halfTextSize.x, halfTextSize.y * 2 - halfSize.y);
                    break;
                case TextAnchor.LowerRight:
                    text.GetComponent<RectTransform>().anchoredPosition = new Vector2(halfSize.x - halfTextSize.x * 2, halfTextSize.y * 2 - halfSize.y);
                    break;
            }
        }

        private Vector2 GetTextSize()
        {
            if (digitImgs.Count == 0)
            {
                return Vector2.zero;
            }

            Vector2 firstDigitImgPos = digitImgs[0].GetComponent<RectTransform>().anchoredPosition;
            Vector2 lastDigitImgPos = digitImgs[digitImgs.Count - 1].GetComponent<RectTransform>().anchoredPosition;

            return Tool.Abs(lastDigitImgPos - firstDigitImgPos) + digitImgSize;
        }
    }
}
