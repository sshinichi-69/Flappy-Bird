using FlappyBird.UI;
using UnityEngine;

namespace FlappyBird.GameState
{
    public class MenuState : GameState
    {
        public override void SetNewState()
        {
            GameStateType = GameStateType.MENU;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                    activity.Call<bool>("moveTaskToBack", true);
                }
                else
                {
                    Application.Quit();
                }
                return;
            }

            if (UiManager.Instance.IsOnNavigationScreen())
            {
                if (Tool.IsScreenPressed())
                {
                    gameManager.ResetCurrentWaitToDemoTime();
                }
                else
                {
                    gameManager.CountNextCurrentWaitToDemoTime();
                }
            }
            else
            {
                gameManager.ResetCurrentWaitToDemoTime();
            }
        }

        public override void SwitchToStartingState()
        {
            gameManager.ChangeState(new StartState());
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnStartGame();
            }
        }

        public override void SwitchToPlayingGameState() { }

        public override void SwitchToPlayingDemoState()
        {
            gameManager.ChangeState(new PlayDemoState());
            foreach (ElementManager elementManager in gameManager.ElementManagers)
            {
                elementManager.OnPlayDemo();
            }
        }

        public override void SwitchToPausingState() { }

        public override void SwitchToEndingState() { }

        public override void SwitchToMenuState() { }
    }
}
