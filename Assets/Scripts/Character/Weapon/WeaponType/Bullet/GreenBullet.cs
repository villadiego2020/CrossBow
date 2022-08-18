using Win.DamageSystem;
using Win.Enum;
using Win.Ground;
using UnityEngine;

namespace Win.Character
{
    public class GreenBullet : Bullet
    {
        [SerializeField] private LayerMask m_ReflectionLayerMask;
        private int m_CurrentReflection;

        public override void OnCollisionEnter(Collision collision)
        {
            Reflect(collision);

            if ((m_LayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Damageable damageable = collision.transform.gameObject.GetComponent<Damageable>();
                ApplyDamage(damageable);
            }

            if ((m_ReflectionLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                m_CurrentReflection++;
                if (m_CurrentReflection == m_ReflectAmount)
                {
                    Return();
                }
            }
        }

        public override void OnTriggerEnter(Collider other)
        {

        }

        private void Reflect(Collision collision)
        {
            float velocity = m_LastVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(m_LastVelocity.normalized, collision.contacts[0].normal);

            m_Rigidbody.velocity = direction * velocity;
        }

        public override void Update()
        {
            base.Update();

            if (transform.position.z <= GroundController.Instance.BoundsMinZ)
            {
                Return();
            }
        }
    }
}
