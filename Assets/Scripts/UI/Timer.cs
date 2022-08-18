using System;
using UnityEngine;
using UnityEngine.UI;

namespace Win.UI
{
    public class Timer : MonoBehaviour
    {
        public Action Timeout;
        [SerializeField] private float m_Timer = 60f;
        [SerializeField] private bool m_Start = false;
        [SerializeField] private Text m_TimeText;

        private void Update()
        {
            if (!m_Start) return;

            if (m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;

                double time = System.Math.Round(m_Timer, 2);
                m_TimeText.text = $"{time}";
            }
            else
            {
                m_Start = false;
                Timeout?.Invoke();
                Active(false);
            }
        }

        public void Count()
        {
            Active(true);
            m_Timer = 60f;
            m_Start = true;
        }

        public void Stop()
        {
            m_Start = false;
            Active(false);
        }

        public void Active(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}