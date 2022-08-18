using Win.Enum;
using UnityEngine;

namespace Win.Character
{
    public class GreenWeapon : WeaponComponent
    {
        public override WeaponType WeaponType => WeaponType.Green;

        public override void EquipTo<T>(T target)
        {
            base.EquipTo(target);

            m_ReflectAmount = (int)m_UnitAttribute.GetValue(UnitStatKey.ReflectionAmount);
        }
    }
}
