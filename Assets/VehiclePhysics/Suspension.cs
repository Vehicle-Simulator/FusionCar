using System;
using UnityEngine;

namespace VehiclePhysics
{
    [Serializable]
    public struct Suspension
    {
        [SerializeField] private float _suspensionLength;
        [SerializeField] private int _damper;
        [SerializeField] private int _springConstant;

        [SerializeField] private float _raycastLength;
        private float _lastSuspensionCompression;
        public int SpringConstant => _springConstant;
        public Transform SuspensionTransform;

        public void SetSuspensionTransform(Transform transform)
        {
            SuspensionTransform = transform;
        }

        public void SetRaycastLength(float balancingLength, Wheel wheel)
        {
            _raycastLength = balancingLength + wheel.Radius + _suspensionLength;
        }


        public float ApplySpringForce(Rigidbody rigidbody, out RaycastHit raycastHit)
        {
            if (!Physics.Raycast(SuspensionTransform.position, -SuspensionTransform.up, out raycastHit,
                    _raycastLength)) return 0;
            var springCompression = _raycastLength - raycastHit.distance;
            var springSpeed = (_lastSuspensionCompression - springCompression) / Time.fixedDeltaTime;
            _lastSuspensionCompression = springCompression;
            var springForce = SpringConstant * springCompression - _damper * springSpeed;
            rigidbody.AddForceAtPosition(
                springForce * SuspensionTransform.up,
                SuspensionTransform.position, ForceMode.Acceleration);

            return springForce;
        }
    }
}