using Win.Input;
using System;
using UnityEngine;

namespace Win.UI
{
    public class Begin : MonoBehaviour
    {
        public Action StartGame;

        private void Start()
        {
            UserInput.Instance.PressAnyKey = StartGameplay;
        }

        private void StartGameplay()
        {
            if (!isActiveAndEnabled)
                return;

            StartGame?.Invoke();
            Active(false);
        }

        public void Active(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
