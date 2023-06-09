using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace VehiclePhysics
{
    public class SuspensionBehaviour : MonoBehaviour
    {
        [SerializeField] private Suspension _suspension;
        public Suspension Suspension => _suspension;
    }

    [Serializable]
    public struct Suspension
    {
        [SerializeField] private Wheel _wheel;
        [SerializeField] private float _suspensionLength;
        [SerializeField] private int _damper;
        [SerializeField] private int _springConstant;

        private float _raycastLength;
        private float _lastSuspensionLength;

        public int SpringConstant => _springConstant;
        public Transform SuspensionTransform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }


        public void SetSuspensionTransform(Transform transform)
        {
            SuspensionTransform = transform;
        }

        public void SetRaycastLength(float balancingLength)
        {
            _raycastLength = balancingLength + _wheel.Radius + _suspensionLength;
        }

        public void SetRigidbody(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        public float ApplySpringForce()
        {
            var springLength = _raycastLength;
            if (Physics.Raycast(SuspensionTransform.position, -SuspensionTransform.up, out var raycastHit,
                    _raycastLength))
            {
                var springCompression = _raycastLength - raycastHit.distance;
                var springSpeed = (_lastSuspensionLength - springCompression) / Time.fixedDeltaTime;
                _lastSuspensionLength = springCompression;

                Rigidbody.AddForceAtPosition(
                    (SpringConstant * springCompression - _damper * springSpeed) * SuspensionTransform.up,
                    SuspensionTransform.position, ForceMode.Acceleration);
                springLength = raycastHit.distance;
                Debug.LogError(SpringConstant);
            }

            return springLength;
        }
    }
}