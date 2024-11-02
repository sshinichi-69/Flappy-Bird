using FlappyBird.Data;
using FlappyBird.InGame;
using FlappyBird.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        [SerializeField] private Field field;
        [SerializeField] private Player player;
        [SerializeField] private Bot bot;

        private readonly float waitToDemoTime = 5f;

        private GameState gameState;
        private Bird currentBird;
        private float currentWaitToDemoTime = 0f;
        private int score = 0;
        private HighScore highScore;

        private AudioSource audioSource;

        private void Awake()
        {
            gameState = GameState.MENU;
            currentBird = player;

            LoadData();

            audioSource = GetComponent<AudioSource>();

            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
        }

        void Update()
        {
            switch (gameState)
            {
                case GameState.MENU:
                    if (UiManager.Instance.IsOnNavigationScreen())
                    {
                        if (Tool.IsScreenPressed())
                        {
                            currentWaitToDemoTime = 0f;
                        }
                        else
                        {
                            currentWaitToDemoTime += Time.deltaTime;
                            if (currentWaitToDemoTime >= waitToDemoTime)
                            {
                                currentWaitToDemoTime = 0f;
                                SwitchToPlayingDemoState();
                            }
                        }
                    }
                    else
                    {
                        currentWaitToDemoTime = 0f;
                    }
                    break;
                case GameState.STARTING:
                    if (Tool.IsScreenPressed())
                    {
                        SwitchToPlayingGameState();
                    }
                    break;
                case GameState.PLAYING_GAME:
                    CheckAddingScore();
                    break;
                case GameState.PLAYING_DEMO:
                    CheckAddingScore();
                    if (Tool.IsScreenPressed())
                    {
                        SwitchToMenuState();
                    }
                    break;
                case GameState.PAUSING:
                    if (Tool.IsScreenPressed())
                    {
                        SwitchToPlayingGameState();
                    }
                    break;
            }
        }

        public void SwitchToStartingState()
        {
            if (gameState == GameState.MENU)
            {
                gameState = GameState.STARTING;
                currentBird.gameObject.SetActive(true);
                UiManager.Instance.OnStartGame();
            }
        }

        public void SwitchToPlayingGameState()
        {
            if (gameState == GameState.STARTING)
            {
                gameState = GameState.PLAYING_GAME;
                currentBird.OnPlayGame();
                PipeManager.Instance.OnPlayGame();
                UiManager.Instance.OnPlayGame();
            }
            else
            {
                gameState = GameState.PLAYING_GAME;
                Time.timeScale = 1;
                UiManager.Instance.OnResumeGame();
            }
        }

        public void SwitchToPlayingDemoState()
        {
            if (gameState == GameState.MENU)
            {
                gameState = GameState.PLAYING_DEMO;
                currentBird = bot;
                currentBird.gameObject.SetActive(true);
                currentBird.OnPlayGame();
                UiManager.Instance.OnPlayDemo();
                PipeManager.Instance.OnPlayGame();
            }
        }

        public void SwitchToPausingState()
        {
            if (gameState == GameState.PLAYING_GAME)
            {
                gameState = GameState.PAUSING;
                Time.timeScale = 0;
                UiManager.Instance.OnPauseGame();
            }
        }

        public void SwitchToEndingState()
        {
            if (gameState == GameState.PLAYING_GAME)
            {
                gameState = GameState.ENDING;
                HighScore.SetNewScore(score);
                currentBird.OnEndGame();
                UiManager.Instance.OnEndGame();
                SaveData();
            }
        }

        public void SwitchToMenuState()
        {
            if (gameState == GameState.ENDING || gameState == GameState.PLAYING_DEMO)
            {
                score = 0;
                currentBird.OnBackToMenu();
                currentBird.gameObject.SetActive(false);
                PipeManager.Instance.OnBackToMenu();
                if (gameState == GameState.ENDING)
                {
                    UiManager.Instance.OnBackToMenu();
                }
                else if (gameState == GameState.PLAYING_DEMO)
                {
                    currentBird = player;
                    UiManager.Instance.OnEndDemo();
                }
                gameState = GameState.MENU;
            }
        }

        public bool IsPlayingState()
        {
            return gameState == GameState.PLAYING_GAME || gameState == GameState.PLAYING_DEMO;
        }

        public void AddScore()
        {
            score++;
            UiManager.Instance.SetScore(score);
            audioSource.Play();
        }

        void CheckAddingScore()
        {
            float xCheckingPipePos = PipeManager.Instance.GetXFirstPipeObjectivePos();
            float xPlayerPos = currentBird.GetXPos();
            if (xPlayerPos > xCheckingPipePos)
            {
                AddScore();
                PipeManager.Instance.RemovePipeObjective();
            }
        }

        void LoadData()
        {
            SaveData data = SaveSystem.Load();
            highScore = new HighScore(data.HighScores);
        }

        void SaveData()
        {
            SaveSystem.Save();
        }

        public Field Field { get { return field; } }
        public GameState GameState { get { return gameState; } }
        public HighScore HighScore { get { return highScore; } }
        public int Score { get { return score; } }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }
                return instance;
            }
        }
    }
}
