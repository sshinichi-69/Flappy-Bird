using UnityEngine;

namespace FlappyBird.UI
{
    public abstract class UiElement : MonoBehaviour
    {
        private void Awake()
        {
            ResponsiveUi();
        }

        protected abstract void ResponsiveUi();
    }
}
