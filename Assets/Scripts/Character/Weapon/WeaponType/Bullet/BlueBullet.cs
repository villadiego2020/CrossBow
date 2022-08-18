using Win.DamageSystem;
using UnityEngine;

namespace Win.Character
{
    public class BlueBullet : Bullet
    {
        public override void OnCollisionEnter(Collision collision)
        {
            if ((m_LayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                Damageable damageable = collision.transform.gameObject.GetComponent<Damageable>();
                ApplyDamage(damageable);
            }

            Return();
        }

        public override void OnTriggerEnter(Collider other)
        {

        }
    }
}
