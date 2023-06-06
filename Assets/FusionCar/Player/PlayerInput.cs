using Fusion;

namespace Player
{
    public struct PlayerInput : INetworkInput
    {
        public NetworkButtons NetworkButtons;
        public float SteeringValue;
        public float Acceleration;
        public float Breaking;
    }
}