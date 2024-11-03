using UnityEngine;

namespace FlappyBird.GameState
{
    public class PlayGameState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.PLAYING_GAME;
        }

        public override void Update()
        {
            gameManager.CheckAddingScore();
        }

        public override void SwitchToStartingState() { }

        public override void SwitchToPlayingGameState() { }

        public override void SwitchToPlayingDemoState() { }

        public override void SwitchToPausingState()
        {
            gameManager.ChangeState(new PauseState());
            Time.timeScale = 0;
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnPauseGame();
            }
        }

        public override void SwitchToEndingState()
        {
            gameManager.ChangeState(new EndState());
            gameManager.SetNewScore();
            gameManager.SaveData();
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnEndGame();
            }
        }

        public override void SwitchToMenuState() { }
    }
}
