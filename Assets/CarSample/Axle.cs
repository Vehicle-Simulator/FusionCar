using UnityEngine;

namespace CarSample
{
    public class Axle : MonoBehaviour
    {
        [SerializeField] private WheelCollider _leftWheel;
        [SerializeField] private WheelCollider _rightWheel;

        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _accelerationRatio;
        [SerializeField] private int _antiRollingForce;

        public void UpdateAxle(Rigidbody rigidbody, float steeringValue, float acceleration)
        {
            UpdateWheel(_leftWheel, rigidbody, acceleration);
            UpdateWheel(_rightWheel, rigidbody, acceleration);
            ApplyAntiRollingForce(rigidbody);
            UpdateSteering(_leftWheel, steeringValue);
            UpdateSteering(_rightWheel, steeringValue);
        }

        private void UpdateWheel(WheelCollider wheelCollider, Rigidbody rigidbody, float acceleration)
        {
            var wheelTransform = wheelCollider.transform;
            var wheelForce = wheelTransform.up * wheelCollider.CalculateSpringForce();
            wheelCollider.UpdateWheelTelemetry();
            if (wheelCollider.IsGrounded == false) return;
            var frictionForce = wheelCollider.CalculateFriction();
            var sidewaysFriction = (frictionForce.x) * wheelTransform.right;
            var accelerationForce = wheelTransform.forward * acceleration * _accelerationRatio;

            rigidbody.AddForceAtPosition(wheelForce + sidewaysFriction + accelerationForce, wheelTransform.position,
                ForceMode.Acceleration);
        }

        private void UpdateSteering(WheelCollider wheelCollider, float steeringValue)
        {
            wheelCollider.UpdateSteerAngle(steeringValue * _maxSteerAngle);
        }

        private void ApplyAntiRollingForce(Rigidbody rigidbody)
        {
            if (!_leftWheel.IsGrounded || !_rightWheel.IsGrounded) return;
            var suspensionDelta = _rightWheel.SuspensionLength - _leftWheel.SuspensionLength;
            var antiRollingForce = _antiRollingForce * suspensionDelta;
            var up = transform.up;
            rigidbody.AddForceAtPosition(up * antiRollingForce, _leftWheel.transform.position,
                ForceMode.Acceleration);
            rigidbody.AddForceAtPosition(-up * antiRollingForce, _rightWheel.transform.position,
                ForceMode.Acceleration);
        }
    }
}