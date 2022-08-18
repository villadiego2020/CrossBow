using Win.Enum;

namespace Win.DamageSystem
{
    public interface IMessageReceiver
    {
        void OnReceiveMessage(MessageType type);
    }
}
