using Win.Ground;
using Win.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Win.Character
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(WeaponHolder))]
    public class CharacterBehavior : MonoBehaviour
    {
        private CharacterMovement m_CharacterMovement;
        private WeaponHolder m_WeaponHolder;
        private bool m_IsFire;

        private void Start()
        {
            UserInput.Instance.Fire = Fire;
        }

        public void Init()
        {
            m_CharacterMovement = GetComponent<CharacterMovement>();
            m_WeaponHolder = GetComponent<WeaponHolder>();
            m_IsFire = false;
            m_CharacterMovement.Init();
            m_WeaponHolder.SwitchDefaultWeapon();
        }

        private void Update()
        {
            if (!GroundController.Instance.IsStart)
                return;

            if (m_IsFire)
            {
                m_WeaponHolder.CurrentWeapon.Fire();
            }
        }

        void Fire(InputValue value)
        {
            if (!GroundController.Instance.IsStart)
                return;

            if (value.isPressed)
            {
                m_IsFire = true;
            }
            else
            {
                m_IsFire = false;
            }
        }
    }
}
