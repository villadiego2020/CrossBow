using Win.Attribute;
using Win.DamageSystem;
using Win.Enum;
using System;
using UnityEngine;

namespace Win.Character
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected LayerMask m_LayerMask;

        public Action<GameObject> BackToPool;

        protected Rigidbody m_Rigidbody;
        protected Vector3 m_LastVelocity;

        [Header("Data")]
        [SerializeField] protected float m_BulletForce;
        [SerializeField] protected int m_BulletDamagee;
        [SerializeField] protected int m_ReflectAmount;

        public void Apply(Transform point, float bulletForce, int bulletDamage, int reflectAmount)
        {
            m_BulletForce = bulletForce;
            m_BulletDamagee = bulletDamage;
            m_ReflectAmount = reflectAmount;
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.AddForce(point.forward * m_BulletForce, ForceMode.VelocityChange);
        }

        public virtual void Update()
        {
            m_LastVelocity = m_Rigidbody.velocity;
        }

        public abstract void OnTriggerEnter(Collider other);
        public abstract void OnCollisionEnter(Collision collision);

        public void ApplyDamage(Damageable damageable)
        {
            DamageMessage damageMessage = new DamageMessage()
            {
                Damage = m_BulletDamagee
            };

            damageable.ApplyDamage(damageMessage);
        }

        public void Return()
        {
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            m_Rigidbody.Sleep();

            BackToPool?.Invoke(gameObject);
        }
    }
}
