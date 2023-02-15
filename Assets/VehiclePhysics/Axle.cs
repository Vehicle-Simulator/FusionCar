using System;
using UnityEngine;

namespace VehiclePhysics
{
    [Serializable]
    public class Axle
    {
        [Header("Wheels")] public Wheel WheelLeft;
        public Transform WheelTransformLeft;
        public Suspension suspensionLeft;

        public Wheel WheelRight;
        public Transform WheelTransformRight;
        public Suspension SuspensionRight;

        [Header("Axle Properties")] public bool SteerableWheel;
        public bool DriveWheel;
        public int AntiRollingSpringConstant;
        public float AxleWidth;


#if UNITY_EDITOR
        [SerializeField] private SuspensionBehaviour _suspensionBehaviourLeft;
        [SerializeField] private SuspensionBehaviour _suspensionBehaviourRight;

        public void UpdateAxleWidth(float axleWidth)
        {
            AxleWidth = axleWidth;
        }

        public void SetSuspension()
        {
            suspensionLeft = _suspensionBehaviourLeft.Suspension;
            SuspensionRight = _suspensionBehaviourRight.Suspension;

            WheelLeft = _suspensionBehaviourLeft.Wheel;
            WheelRight = _suspensionBehaviourRight.Wheel;

            suspensionLeft.SetSuspensionTransform(_suspensionBehaviourLeft.transform);
            SuspensionRight.SetSuspensionTransform(_suspensionBehaviourRight.transform);
        }

        public void DestroySuspensionBehaviour()
        {
            _suspensionBehaviourLeft.Dispose();
            _suspensionBehaviourRight.Dispose();
            _suspensionBehaviourLeft = _suspensionBehaviourRight = null;
        }
#endif
    }
}