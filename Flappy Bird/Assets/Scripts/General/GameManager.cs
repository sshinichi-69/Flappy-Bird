using FlappyBird.Data;
using FlappyBird.GameState;
using FlappyBird.InGame;
using FlappyBird.UI;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        [SerializeField] private Field field;

        private readonly float waitToDemoTime = 5f;

        private List<ElementManager> elementManagers;
        private GameState.GameState gameState;
        private float currentWaitToDemoTime = 0f;
        private int score = 0;
        private HighScore highScore;

        private AudioSource audioSource;

        private void Awake()
        {
            gameState = new MenuState();

            LoadData();

            audioSource = GetComponent<AudioSource>();

            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
        }

        private void Start()
        {
            elementManagers = new List<ElementManager>();
            elementManagers.Add(BirdManager.Instance);
            elementManagers.Add(PipeManager.Instance);
            elementManagers.Add(UiManager.Instance);
        }

        void Update()
        {
            gameState.Update();
        }

        public void ChangeState(GameState.GameState state)
        {
            gameState = state;
        }

        public void StartGame()
        {
            gameState.SwitchToStartingState();
        }

        public void Pause()
        {
            gameState.SwitchToPausingState();
        }

        public void EndGame()
        {
            gameState.SwitchToEndingState();
        }

        public void BackToMenu()
        {
            gameState.SwitchToMenuState();
        }

        public bool IsPlayingState()
        {
            return GameStateType == GameStateType.PLAYING_GAME || GameStateType == GameStateType.PLAYING_DEMO;
        }

        public void AddScore()
        {
            score++;
            UiManager.Instance.SetScore(score);
            audioSource.Play();
        }

        public void ResetCurrentWaitToDemoTime()
        {
            currentWaitToDemoTime = 0f;
        }

        public void CountNextCurrentWaitToDemoTime()
        {
            currentWaitToDemoTime += Time.deltaTime;
            if (currentWaitToDemoTime >= waitToDemoTime)
            {
                currentWaitToDemoTime = 0f;
                gameState.SwitchToPlayingDemoState();
            }
        }

        public void CheckAddingScore()
        {
            float xCheckingPipePos = PipeManager.Instance.GetXFirstPipeObjectivePos();
            float xPlayerPos = BirdManager.Instance.GetXPos();
            if (xPlayerPos > xCheckingPipePos)
            {
                AddScore();
                PipeManager.Instance.RemovePipeObjective();
            }
        }

        public void SetNewScore()
        {
            highScore.SetNewScore(score);
        }

        public void ResetScore()
        {
            score = 0;
        }

        public void LoadData()
        {
            SaveData data = SaveSystem.Load();
            highScore = new HighScore(data.HighScores);
        }

        public void SaveData()
        {
            SaveSystem.Save();
        }

        public Field Field { get { return field; } }
        public List<ElementManager> ElementManagers { get { return elementManagers; } }
        public HighScore HighScore { get { return highScore; } }
        public int Score { get { return score; } }

        public GameStateType GameStateType
        {
            get
            {
                if (gameState == null)
                {
                    return GameStateType.PAUSING;
                }
                return gameState.GameStateType;
            }
        }

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
