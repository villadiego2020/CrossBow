using Win.Enum;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Win.DamageSystem
{
    public class Damageable : MonoBehaviour
    {
        [MonoStore(typeof(IMessageReceiver))]
        public List<MonoInterfaceStore<IMessageReceiver>> OnDamageMessageReceivers;
        public int CurrentHitPoints { get; private set; }

        public void ApplyDamage(DamageMessage message)
        {
            if (CurrentHitPoints <= 0)
                return;

            ReceiveDamage(message.Damage);

            MessageType messageType = CurrentHitPoints <= 0 ? MessageType.Dead : MessageType.Damaged;

            foreach (MonoInterfaceStore<IMessageReceiver> responder in OnDamageMessageReceivers)
            {
                responder.Value.OnReceiveMessage(messageType);
            }
        }

        public void AdjustHp(int hp)
        {
            CurrentHitPoints = hp;
        }

        private void ReceiveDamage(int damage)
        {
            CurrentHitPoints = CurrentHitPoints - damage;
        }
    }
}