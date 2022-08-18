using Win.Attribute;
using Win.DamageSystem;
using Win.Enum;
using UnityEngine;

namespace Win.Obstacle
{
    [RequireComponent(typeof(UnitSetupStat))]
    public class ObstacleBehavior : MonoBehaviour, IMessageReceiver
    {
        protected UnitSetupStat m_UnitAttribute;

        public bool IsDestroy { get; protected set; }

        public virtual void Start()
        {
        }

        public virtual void Init()
        {
            m_UnitAttribute = GetComponent<UnitSetupStat>();
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

        public virtual void ApplyDamage() { }
        public virtual void ApplyDead() { }
    }
}