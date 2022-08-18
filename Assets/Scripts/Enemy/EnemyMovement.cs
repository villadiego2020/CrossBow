using Win.Attribute;
using Win.Enum;
using Win.Ground;
using UnityEngine;

namespace Win.Enemy
{
    [RequireComponent(typeof(UnitSetupStat))]
    public class EnemyMovement : MonoBehaviour
    {
        private UnitSetupStat m_UnitAttribute;

        private Vector3 m_Point;
        private float m_Angle;

        [Header("Data")]
        [SerializeField] private float m_RotationSpeed;
        [SerializeField] private float m_RotationRadius;

        private Vector3 m_TmpPosition;

        private void Start()
        {
            m_TmpPosition = transform.position;
        }

        public void Init()
        {
            transform.position = m_TmpPosition;

            m_UnitAttribute = GetComponent<UnitSetupStat>();
            m_Point = transform.position;

            m_RotationSpeed = m_UnitAttribute.GetValue(UnitStatKey.RotationSpeed);
            m_RotationRadius = m_UnitAttribute.GetValue(UnitStatKey.RotationRadius);
        }

        private void Update()
        {
            if (!GroundController.Instance.IsStart)
                return;

            m_Angle += m_RotationSpeed * Time.deltaTime;

            transform.position = m_Point + new Vector3(Mathf.Sin(m_Angle), 0, Mathf.Cos(m_Angle)) * m_RotationRadius;
        }
    }
}