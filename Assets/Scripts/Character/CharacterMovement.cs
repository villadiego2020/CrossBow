using Win.Attribute;
using Win.Enum;
using Win.Ground;
using Win.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Win.Character
{
    [RequireComponent(typeof(UnitSetupStat))]
    public class CharacterMovement : MonoBehaviour
    {
        private UnitSetupStat m_UnitAttribute;

        private float m_MaxAngle = 70f;

        private Move m_Move;
        private bool m_IsPressed;

        [Header("Data")]
        [SerializeField] private float m_RotateSpeed;

        private void Start()
        {
            UserInput.Instance.MoveLeft = MoveLeft;
            UserInput.Instance.MoveRight = MoveRight;
        }

        public void Init()
        {
            m_UnitAttribute = GetComponent<UnitSetupStat>();
            m_RotateSpeed = m_UnitAttribute.GetValue(UnitStatKey.RotationSpeed);
        }

        private void Update()
        {
            if (!GroundController.Instance.IsStart)
                return;

            if (m_IsPressed)
            {
                float angle = GetAngle();
                transform.eulerAngles = new Vector3(0f, Mathf.Clamp(angle, -m_MaxAngle, m_MaxAngle), 0f);
            }
        }

        private float GetAngle()
        {
            float angle = (transform.eulerAngles.y + ((m_Move == Move.Left ? -m_RotateSpeed : +m_RotateSpeed) * Time.deltaTime));
            angle = (angle > 180f) ? angle - 360f : angle;

            return angle;
        }

        private void MoveLeft(InputValue value)
        {
            if (!GroundController.Instance.IsStart)
                return;

            m_Move = Move.Left;
            m_IsPressed = value.isPressed;
        }

        private void MoveRight(InputValue value)
        {
            if (!GroundController.Instance.IsStart)
                return;

            m_Move = Move.Right;
            m_IsPressed = value.isPressed;
        }
    }
}