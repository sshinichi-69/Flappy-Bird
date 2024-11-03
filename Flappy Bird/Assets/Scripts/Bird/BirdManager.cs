using UnityEngine;

namespace FlappyBird.InGame
{
    public class BirdManager : ElementManager
    {
        private static BirdManager instance;

        [SerializeField] private Player player;
        [SerializeField] private Bot bot;

        private Bird currentBird;

        private void Awake()
        {
            currentBird = player;

            if (instance == null)
            {
                instance = FindObjectOfType<BirdManager>();
            }
        }

        public override void OnStartGame()
        {
            currentBird.gameObject.SetActive(true);
        }

        public override void OnPlayGame()
        {
            currentBird.OnPlayGame();
        }

        public override void OnPlayDemo()
        {
            currentBird = bot;
            currentBird.gameObject.SetActive(true);
            currentBird.OnPlayGame();
        }

        public override void OnEndDemo()
        {
            currentBird.OnBackToMenu();
            currentBird.gameObject.SetActive(false);
            currentBird = player;
        }

        public override void OnPauseGame() { }
        public override void OnResumeGame() { }

        public override void OnEndGame()
        {
            currentBird.OnEndGame();
        }

        public override void OnBackToMenu()
        {
            currentBird.OnBackToMenu();
            currentBird.gameObject.SetActive(false);
        }

        public float GetXPos()
        {
            return currentBird.GetXPos();
        }

        public static BirdManager Instance {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BirdManager>();
                }
                return instance;
            }
        }
    }
}
