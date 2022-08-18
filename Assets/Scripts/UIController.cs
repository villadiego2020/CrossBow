using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Win.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    m_Instance = (UIController)FindObjectOfType(typeof(UIController));
                }

                return m_Instance;
            }
        }

        protected static UIController m_Instance;

        [SerializeField] private Timer m_Timer;
        [SerializeField] private Begin m_Begin;
        [SerializeField] private PlayerAgain m_PlayerAgain;

        public Action StartGame;
        public Action Timeout;

        private void Start()
        {
            m_Timer.Timeout = TimeoutGame;
            m_Begin.StartGame = TapToStart;
            m_PlayerAgain.StartGame = TapToStart;
        }

        private void TimeoutGame()
        {
            Timeout?.Invoke();
        }

        public void TapToStart()
        {
            m_Timer.Count();
            StartGame?.Invoke();
        }

        public void EndGame()
        {
            m_Timer.Stop();
            m_PlayerAgain.Active(true);
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
