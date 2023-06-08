using Fusion;

namespace CarSample.Scripts.Player
{
    public struct PlayerCarInputNetworkStruct : INetworkInput
    {
        public NetworkButtons NetworkButtons;
        public float SteeringValue;
        public float Acceleration;
        public float Breaking;
    }
}