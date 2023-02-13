using System;
using System.Collections.Generic;
using UnityEngine;

namespace VehiclePhysics
{
    public class DriveTrain : MonoBehaviour
    {
        [SerializeField] private List<Axle> _axles;
        public IReadOnlyList<Axle> Axles => _axles;

        private Rigidbody _rigidbody;
#if UNITY_EDITOR

        private void OnValidate()
        {
            RefreshData();
        }

        public void RefreshData()
        {
            var totalSpringConstant = 0;
            _rigidbody = GetComponentInParent<Rigidbody>();
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                var axle = _axles[idx];
                axle.SetSuspension(_rigidbody);
                totalSpringConstant += axle.suspensionLeft.SpringConstant;
                totalSpringConstant += axle.SuspensionRight.SpringConstant;
                var axleWidth = axle.SuspensionRight.SuspensionTransform.localPosition.x -
                                axle.suspensionLeft.SuspensionTransform.localPosition.x;
                axle.UpdateAxleWidth(axleWidth);
            }

            var balancingLength = Physics.gravity.magnitude / totalSpringConstant;
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                Axle axle = _axles[idx];
                axle.suspensionLeft.SetRaycastLength(balancingLength, axle.WheelLeft);
                axle.SuspensionRight.SetRaycastLength(balancingLength, axle.WheelRight);
                _axles[idx] = axle;
            }
        }
#endif
        private void Start()
        {
#if UNITY_EDITOR
            foreach (var axle in _axles)
            {
                axle.DestroySuspensionBehaviour();
            }
#endif
        }

        private void FixedUpdate()
        {
            foreach (var axle in _axles)
            {
                var springForce = axle.suspensionLeft.ApplySpringForce(out var raycastHitLeft);
                var frictionForceLeft =
                    axle.WheelLeft.ApplyFriction(axle.suspensionLeft.SuspensionTransform, springForce);
//                Debug.LogError(frictionForceLeft);
                var frictionForceLeftVector =
                    frictionForceLeft.sideways * axle.suspensionLeft.SuspensionTransform.right +
                    frictionForceLeft.forward * axle.suspensionLeft.SuspensionTransform.forward;
                _rigidbody.AddForceAtPosition(frictionForceLeftVector, raycastHitLeft.point,
                    ForceMode.Acceleration);

                springForce = axle.SuspensionRight.ApplySpringForce(out var raycastHitRight);
                var frictionForceRight =
                    axle.WheelRight.ApplyFriction(axle.SuspensionRight.SuspensionTransform, springForce);
                var frictionForceRightVector =
                    frictionForceRight.sideways * axle.SuspensionRight.SuspensionTransform.right +
                    frictionForceRight.forward * axle.SuspensionRight.SuspensionTransform.forward;
                _rigidbody.AddForceAtPosition(frictionForceRightVector, raycastHitRight.point,
                    ForceMode.Acceleration);
            }
        }


        private void UpdateWheel(Axle axle, (RaycastHit raycastHitLeft, RaycastHit raycastHitRight) raycastHits)
        {
        }
    }
}