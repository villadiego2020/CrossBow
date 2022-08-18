using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Win.Input
{
    public class UserInput : MonoBehaviour
    {
        public static UserInput Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    m_Instance = (UserInput)FindObjectOfType(typeof(UserInput));
                }

                return m_Instance;
            }
        }

        protected static UserInput m_Instance;

        public Action PressAnyKey;
        public Action Enter;
        public Action<InputValue> MoveLeft;
        public Action<InputValue> MoveRight;
        public Action SwitchWeapon;
        public Action<InputValue> Fire;
        public Action ESC;

        void OnPressAnyKey(InputValue value)
        {
            PressAnyKey?.Invoke();
        }

        void OnEnter(InputValue value)
        {
            Enter?.Invoke();
        }

        void OnMoveLeft(InputValue value)
        {
            MoveLeft?.Invoke(value);
        }

        void OnMoveRight(InputValue value)
        {
            MoveRight?.Invoke(value);
        }

        void OnSwitchWeapon(InputValue value)
        {
            SwitchWeapon?.Invoke();
        }

        void OnFire(InputValue value)
        {
            Fire?.Invoke(value);
        }

        void OnESC(InputValue value)
        {
            ESC?.Invoke();
        }
    }
}