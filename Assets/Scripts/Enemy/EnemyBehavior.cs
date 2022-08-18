using Win.Attribute;
using Win.DamageSystem;
using Win.Enum;
using Win.Ground;
using System.Collections;
using UnityEngine;

namespace Win.Enemy
{
    [RequireComponent(typeof(UnitSetupStat))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyBehavior : MonoBehaviour, IMessageReceiver
    {
        [SerializeField] private Material m_SelfMaterial;
        [SerializeField] private Material m_HitMaterial;
        [SerializeField] private MeshRenderer m_SelfMesh;

        private Damageable m_Damageable;
        private UnitSetupStat m_UnitAttribute;
        private EnemyMovement m_EnemyMovement;

        [Header("Data")]
        [SerializeField] private int m_Hp;

        public void Init()
        {
            gameObject.SetActive(true);

            m_Damageable = GetComponent<Damageable>();
            m_UnitAttribute = GetComponent<UnitSetupStat>();
            m_EnemyMovement = GetComponent<EnemyMovement>();

            m_SelfMesh.material = m_SelfMaterial;
            m_Hp = (int)m_UnitAttribute.GetValue(UnitStatKey.Hp);
            m_Damageable.AdjustHp(m_Hp);
            m_EnemyMovement.Init();
        }

        public void OnReceiveMessage(MessageType type)
        {
            switch (type)
            {
                case MessageType.Damaged:
                    ApplyDamage();
                    break;
                case MessageType.Dead:
                    ApplyDead();
                    break;
            }
        }

        private void ApplyDamage()
        {
            StartCoroutine(DoChangeMaterial());
            IEnumerator DoChangeMaterial()
            {
                m_SelfMesh.material = m_HitMaterial;
                yield return new WaitForSeconds(0.1f);
                m_SelfMesh.material = m_SelfMaterial;
            }
        }

        private void ApplyDead()
        {
            GroundController.Instance.EndGame();
            gameObject.SetActive(false);
        }
    }
}