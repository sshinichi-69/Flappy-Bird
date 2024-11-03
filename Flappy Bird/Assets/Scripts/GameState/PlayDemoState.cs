namespace FlappyBird.GameState
{
    public class PlayDemoState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.PLAYING_DEMO;
        }

        public override void Update()
        {
            gameManager.CheckAddingScore();
            if (Tool.IsScreenPressed())
            {
                SwitchToMenuState();
            }
        }

        public override void SwitchToStartingState() { }

        public override void SwitchToPlayingGameState() { }

        public override void SwitchToPlayingDemoState() { }

        public override void SwitchToPausingState() { }

        public override void SwitchToEndingState() { }

        public override void SwitchToMenuState()
        {
            gameManager.ChangeState(new MenuState());
            gameManager.ResetScore();
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnEndDemo();
            }
        }
    }
}
