using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject navigation;
        [SerializeField] private HighScorePanel highScore;

        private void Update()
        {
            if (Tool.IsScreenPressed())
            {
                BackToNavigation();
            }
        }

        public void OnStartBtnPressed()
        {
            GameManager.Instance.SwitchToStartingState();
        }

        public void OnScoreBtnPressed()
        {
            navigation.SetActive(false);
            highScore.gameObject.SetActive(true);
        }

        public void BackToNavigation()
        {
            if (highScore.gameObject.activeSelf)
            {
                highScore.gameObject.SetActive(false);
                navigation.SetActive(true);
            }
        }

        public bool IsOnNavigationScreen()
        {
            return navigation.activeSelf;
        }
    }
}
