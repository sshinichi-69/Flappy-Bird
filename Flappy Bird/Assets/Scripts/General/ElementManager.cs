using UnityEngine;

namespace FlappyBird
{
    public abstract class ElementManager : MonoBehaviour
    {
        public abstract void OnStartGame();
        public abstract void OnPlayGame();
        public abstract void OnPlayDemo();
        public abstract void OnEndDemo();
        public abstract void OnPauseGame();
        public abstract void OnResumeGame();
        public abstract void OnEndGame();
        public abstract void OnBackToMenu();
    }
}
