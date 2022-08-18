using Win.Ground;
using Win.Input;
using UnityEngine;
using Utility;

namespace Win.Character
{
    public class WeaponHolder : MonoBehaviour, IWeaponHolder
    {
        [MonoStore(typeof(IWeapon))]
        public MonoInterfaceStore<IWeapon>[] m_Weapons;

        public IWeapon CurrentWeapon { get; private set; }
        private int m_Index;


        void Start()
        {
            UserInput.Instance.SwitchWeapon = SwitchWeapon;
        }

        public void SwitchDefaultWeapon()
        {
            m_Index = 0;
            int index = GetWeaponIndex();
            IWeapon weapon = m_Weapons[index].Value;

            if (null != weapon)
                EquipWeapon(weapon);
        }

        public void SwitchWeapon()
        {
            if (!GroundController.Instance.IsStart)
                return;

            EquipWeapon(m_Weapons[GetWeaponIndex()].Value);
        }

        public bool EquipWeapon<T>(T weapon) where T : IWeapon
        {
            UnequipCurrentWeapon();
            CurrentWeapon = weapon;
            weapon.EquipTo(this);

            return true;
        }

        private void UnequipCurrentWeapon()
        {
            if (null != CurrentWeapon)
            {
                CurrentWeapon.UnequipFrom(this);
            }
        }

        private int GetWeaponIndex()
        {
            m_Index = m_Index > m_Weapons.Length - 1 ? 0 : m_Index;
            int index = m_Index;
            m_Index++;

            return index;
        }
    }
}
