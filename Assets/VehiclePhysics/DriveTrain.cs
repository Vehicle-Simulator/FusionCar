using System.Collections.Generic;
using UnityEngine;

namespace VehiclePhysics
{
    public class DriveTrain : MonoBehaviour
    {
        private List<AxleInfo> _axles;

        private void OnValidate()
        {
            var totalSpringConstant = 0;
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                var axle = _axles[idx];
                axle.SetSuspension(GetComponentInParent<Rigidbody>());
                totalSpringConstant += axle.WheelColliderLeft.SpringConstant;
                totalSpringConstant += axle.WheelColliderRight.SpringConstant;
                var axleWidth = axle.WheelColliderRight.SuspensionTransform.localPosition.x -
                                axle.WheelColliderLeft.SuspensionTransform.localPosition.x;
                axle.UpdateAxleWidth(axleWidth);
            }

            var balancingLength = Physics.gravity.magnitude / totalSpringConstant;
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                AxleInfo axle = _axles[idx];
                axle.WheelColliderLeft.SetRaycastLength(balancingLength);
                axle.WheelColliderRight.SetRaycastLength(balancingLength);
                _axles[idx] = axle;
            }
        }

        private void FixedUpdate()
        {
            for (int idx = 0; idx < _axles.Count; idx++)
            {
                var axle = _axles[idx];
                var springLengthLeft = axle.WheelColliderLeft.ApplySpringForce();
                var springLengthRight = axle.WheelColliderRight.ApplySpringForce();
            }
        }
    }
}