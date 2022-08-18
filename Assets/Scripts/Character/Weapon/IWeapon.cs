using UnityEngine;

namespace Win.Character
{
    public interface IWeapon
    {
        Transform transform { get; }
        void EquipTo<T>(T target) where T : IWeaponHolder;
        void UnequipFrom<T>(T target) where T : IWeaponHolder;
        GameObject CreateBullet();
        void Fire();
        void Return(GameObject bulletObject);
    }
}
