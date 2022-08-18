using Win.Attribute;
using Win.Enum;
using Win.Ground;
using UnityEngine;

namespace Win.Obstacle
{
    [RequireComponent(typeof(UnitSetupStat))]
    public class RedObstacle : ObstacleBehavior
    {
        [SerializeField] private bool m_Reverse;

        [Header("Data")]
        [SerializeField] private float m_RotationSpeed;

        public override void Init()
        {
            base.Init();

            m_RotationSpeed = m_UnitAttribute.GetValue(UnitStatKey.RotationSpeed);
        }

        private void Update()
        {
            if (!GroundController.Instance.IsStart)
                return;

            float angle = GetAngle();
            transform.eulerAngles = new Vector3(0f, angle, 0f);
        }

        private float GetAngle()
        {
            float angle = (transform.eulerAngles.y + ((m_Reverse ? -m_RotationSpeed : +m_RotationSpeed) * Time.deltaTime));
            angle = (angle > 180f) ? angle - 360f : angle;

            return angle;
        }
    }
}
