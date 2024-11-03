namespace FlappyBird.GameState
{
    public class StartState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.STARTING;
        }

        public override void Update()
        {
            if (Tool.IsScreenPressed())
            {
                SwitchToPlayingGameState();
            }
        }

        public override void SwitchToStartingState() { }

        public override void SwitchToPlayingGameState()
        {
            gameManager.ChangeState(new PlayGameState());
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnPlayGame();
            }
        }

        public override void SwitchToPlayingDemoState() { }

        public override void SwitchToPausingState() { }

        public override void SwitchToEndingState() { }

        public override void SwitchToMenuState() { }
    }
}
