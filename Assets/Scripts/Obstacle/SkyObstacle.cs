using Win.DamageSystem;
using Win.Enum;
using Win.Ground;
using System.Collections;
using UnityEngine;

namespace Win.Obstacle
{
    [RequireComponent(typeof(Damageable))]
    public class SkyObstacle : ObstacleBehavior
    {
        private Damageable m_Damageable;
        private Vector2 m_Direction;

        [Header("Data")]
        [SerializeField] private int m_Hp;
        [SerializeField] private float m_MovementSpeed;

        [Header("Refference")]
        [SerializeField] private Material m_SelfMaterial;
        [SerializeField] private Material m_HitMaterial;
        [SerializeField] private MeshRenderer m_SelfMesh;

        public override void Init()
        {
            base.Init();

            m_Damageable = GetComponent<Damageable>();
            m_Direction = Random.Range(0f, 1f) > 0.5f ? Vector3.right : Vector3.left;

            m_SelfMesh.material = m_SelfMaterial;
            m_Hp = (int)m_UnitAttribute.GetValue(UnitStatKey.Hp);
            m_MovementSpeed = m_UnitAttribute.GetValue(UnitStatKey.MovementSpeed);

            AdjustHp();
        }

        private void Update()
        {
            if (!GroundController.Instance.IsStart)
                return;

            if (IsDestroy)
                return;

            transform.Translate(m_Direction * m_MovementSpeed * Time.deltaTime);

            if (transform.position.x <= GroundController.Instance.BoundsMinX)
            {
                m_Direction = Vector3.right;
            }
            else if (transform.position.x >= GroundController.Instance.BoundMaxX)
            {
                m_Direction = Vector3.left;
            }
        }

        public override void ApplyDamage()
        {
            StartCoroutine(DoChangeMaterial());
            IEnumerator DoChangeMaterial()
            {
                m_SelfMesh.material = m_HitMaterial;
                yield return new WaitForSeconds(0.1f);
                m_SelfMesh.material = m_SelfMaterial;
            }
        }

        public override void ApplyDead()
        {
            IsDestroy = true;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(DoWaitForSpawn());
            IEnumerator DoWaitForSpawn()
            {
                yield return new WaitForSeconds(2f);

                GetComponent<MeshRenderer>().enabled = true;
                GetComponent<BoxCollider>().enabled = true;
                AdjustHp();
                IsDestroy = false;
            }
        }

        private void AdjustHp()
        {
            m_Damageable.AdjustHp(m_Hp);
        }
    }
}
