using UnityEngine;

namespace FlappyBird.GameState
{
    public class PauseState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.PAUSING;
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
            Time.timeScale = 1;
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnResumeGame();
            }
        }

        public override void SwitchToPlayingDemoState() { }

        public override void SwitchToPausingState() { }

        public override void SwitchToEndingState() { }

        public override void SwitchToMenuState() { }
    }
}
