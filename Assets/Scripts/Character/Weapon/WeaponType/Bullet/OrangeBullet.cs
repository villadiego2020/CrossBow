using Win.DamageSystem;
using Win.Ground;
using UnityEngine;

namespace Win.Character
{
    public class OrangeBullet : Bullet
    {
        public override void OnCollisionEnter(Collision collision)
        {

        }

        public override void OnTriggerEnter(Collider other)
        {
            if ((m_LayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
            {
                Damageable damageable = other.transform.gameObject.GetComponent<Damageable>();
                ApplyDamage(damageable);
            }
        }

        public override void Update()
        {
            base.Update();

            if (transform.position.x <= GroundController.Instance.BoundsMinX ||
                transform.position.x >= GroundController.Instance.BoundMaxX ||
                transform.position.z >= GroundController.Instance.BoundMaxZ)
            {
                Return();
            }
        }
    }
}
