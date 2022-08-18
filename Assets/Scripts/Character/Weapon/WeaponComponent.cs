using Win.Attribute;
using Win.Enum;
using UnityEngine;
using Utility;

namespace Win.Character
{
    [RequireComponent(typeof(UnitSetupStat))]
    public abstract class WeaponComponent : MonoBehaviour, IWeapon
    {
        public UnitSetupStat m_UnitAttribute;

        protected IWeaponHolder m_Owner;
        public abstract WeaponType WeaponType { get; }
        [SerializeField] protected GameObject m_SpawnPoint;
        [SerializeField] protected ObjectPooling m_BulletPool;

        [Header("Data")]
        [SerializeField] protected float m_BulletFireRate;
        [SerializeField] protected float m_BulletForce;
        [SerializeField] protected int m_BulletDamage;
        [SerializeField] protected int m_ReflectAmount;

        protected float m_NextFireRate = 0f;

        public virtual void EquipTo<T>(T target) where T : IWeaponHolder
        {
            gameObject.SetActive(true);

            m_Owner = target;

            m_UnitAttribute = GetComponent<UnitSetupStat>();
            m_BulletFireRate = m_UnitAttribute.GetValue(UnitStatKey.BulletRate);
            m_BulletForce = m_UnitAttribute.GetValue(UnitStatKey.BulletForce);
            m_BulletDamage = (int)m_UnitAttribute.GetValue(UnitStatKey.BulletDamage);
        }

        public virtual void UnequipFrom<T>(T target) where T : IWeaponHolder
        {
            m_Owner = null;

            gameObject.SetActive(false);
        }

        public GameObject CreateBullet()
        {
            GameObject bulletObject = m_BulletPool.GetObject();
            bulletObject.transform.position = m_SpawnPoint.transform.position;
            bulletObject.transform.rotation = m_SpawnPoint.transform.rotation;

            return bulletObject;
        }

        public virtual void Fire()
        {
            if (Time.time > m_NextFireRate)
            {
                m_NextFireRate = Time.time + m_BulletFireRate;

                GameObject bulletObject = CreateBullet();

                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.BackToPool = Return;
                bullet.Apply(m_SpawnPoint.transform, m_BulletForce, m_BulletDamage, m_ReflectAmount);
            }
        }

        public void Return(GameObject bulletObject)
        {
            m_BulletPool.Pool(bulletObject);
        }
    }
}