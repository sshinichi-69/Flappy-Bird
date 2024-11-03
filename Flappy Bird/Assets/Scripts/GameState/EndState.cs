namespace FlappyBird.GameState
{
    public class EndState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.ENDING;
        }

        public override void Update() { }

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
                elementManager.OnBackToMenu();
            }
        }
    }
}
