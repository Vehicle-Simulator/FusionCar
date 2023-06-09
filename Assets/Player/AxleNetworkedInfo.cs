using Fusion;
using UnityEngine;

namespace Player
{
    public struct AxleNetworkedInfo : INetworkStruct
    {
        private Vector3 LeftWheelPosition;
        private Quaternion LeftWheelRotation;
        private Vector3 RightWheelPosition;
        private Quaternion RightWheelRotation;
    }
}