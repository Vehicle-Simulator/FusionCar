using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace VehiclePhysics
{
    public class DriveTrain : NetworkBehaviour
    {
        [SerializeField] private List<Axle> _axles;
        public IReadOnlyList<Axle> Axles => _axles;

        [SerializeField] private Rigidbody _rigidbody;
#if UNITY_EDITOR

        [ContextMenu("Update data")]
        public void RefreshData()
        {
            var totalSpringConstant = 0;
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                var axle = _axles[idx];
                axle.SetSuspension();
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

        public override void FixedUpdateNetwork()
        {
            UpdatePhysics();
        }

        private void UpdatePhysics()
        {
            foreach (var axle in _axles)
            {
                var normalForceLeft = axle.suspensionLeft.ApplySpringForce(_rigidbody, out var raycastHit);
                axle.WheelLeft.ApplyFriction(axle.SuspensionRight.SuspensionTransform, _rigidbody, normalForceLeft,
                    raycastHit.point);
                var normalForceRight = axle.SuspensionRight.ApplySpringForce(_rigidbody, out raycastHit);
                axle.WheelRight.ApplyFriction(axle.SuspensionRight.SuspensionTransform, _rigidbody, normalForceRight,
                    raycastHit.point);
            }
        }
    }
}