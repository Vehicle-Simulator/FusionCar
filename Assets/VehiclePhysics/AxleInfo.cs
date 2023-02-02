using System;
using UnityEngine;

namespace VehiclePhysics
{
    [Serializable]
    public class AxleInfo
    {
        [SerializeField] private SuspensionBehaviour _suspensionBehaviourLeft;
        [SerializeField] private SuspensionBehaviour _suspensionBehaviourRight;

        public Suspension WheelColliderLeft;
        public Transform WheelTransformLeft;


        [HideInInspector] public Suspension WheelColliderRight;
        public Transform WheelTransformRight;

        public bool SteerableWheel;
        public bool DriveWheel;
        public int AntiRollingSpringConstant;
        public float AxleWidth;

        public void UpdateAxleWidth(float axleWidth)
        {
            AxleWidth = axleWidth;
        }

        public void SetSuspension(Rigidbody rigidbody)
        {
            WheelColliderLeft = _suspensionBehaviourLeft.Suspension;
            WheelColliderRight = _suspensionBehaviourRight.Suspension;

            WheelColliderLeft.SetRigidbody(rigidbody);
            WheelColliderRight.SetRigidbody(rigidbody);

            WheelColliderLeft.SetSuspensionTransform(_suspensionBehaviourLeft.transform);
            WheelColliderRight.SetSuspensionTransform(_suspensionBehaviourRight.transform);
        }

        public void OnSuspensionDestroy()
        {
            _suspensionBehaviourLeft = _suspensionBehaviourRight = null;
        }
    }
}