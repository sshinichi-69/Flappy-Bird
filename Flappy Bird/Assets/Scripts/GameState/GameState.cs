namespace FlappyBird.GameState
{
    public abstract class GameState
    {
        protected GameManager gameManager;
        public GameStateType GameStateType { get; protected set; }

        public GameState()
        {
            gameManager = GameManager.Instance;
            SetNewState();
        }

        public abstract void SetNewState();
        public abstract void Update();
        public abstract void SwitchToStartingState();
        public abstract void SwitchToPlayingGameState();
        public abstract void SwitchToPlayingDemoState();
        public abstract void SwitchToPausingState();
        public abstract void SwitchToEndingState();
        public abstract void SwitchToMenuState();
    }
}
