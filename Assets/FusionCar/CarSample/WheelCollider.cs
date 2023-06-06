using Unity.VisualScripting;
using UnityEngine;

namespace CarSample
{
    public class WheelCollider : MonoBehaviour
    {
        [SerializeField] private int _springStrength;
        [SerializeField] private int _springDamping;
        [SerializeField] private float _springLength;
        [SerializeField] private float _frictionCoeficient;

        public bool IsGrounded { get; private set; }
        public float SuspensionLength => _lastSpringLength;

        private Vector3 _wheelSpeed;
        private Vector3 _lastWheelPos;
        private float _lastSpringLength;

        public void Initialize()
        {
            _lastWheelPos = transform.position;
        }

        public float CalculateSpringForce()
        {
            if (Physics.Raycast(transform.position, -transform.up, out var raycastHit, _springLength))
            {
                var springDelta = _springLength - raycastHit.distance;
                var springSpeed = (raycastHit.distance - _lastSpringLength) / Time.fixedDeltaTime;
                var springForce = _springStrength * springDelta - _springDamping * springSpeed;
                _lastSpringLength = raycastHit.distance;
                IsGrounded = true;
                return springForce;
            }

            IsGrounded = false;
            return 0;
        }

        public void UpdateSteerAngle(float angle)
        {
            transform.localEulerAngles = new Vector3(0, angle, 0);
        }

        public Vector2 CalculateFriction()
        {
            var slip = CalculateSlip();
            var opposingForce = -slip;
            return opposingForce * _frictionCoeficient;
        }

        private Vector2 CalculateSlip()
        {
            var sidewaysSlip = Vector3.Dot(_wheelSpeed, transform.right);
            var forwardSlip = Vector3.Dot(_wheelSpeed, transform.forward);
            return new Vector2(sidewaysSlip, forwardSlip);
        }

        public void UpdateWheelTelemetry()
        {
            var position = transform.position;
            _wheelSpeed = (position - _lastWheelPos) / Time.fixedDeltaTime;
            _lastWheelPos = position;
        }
    }
}