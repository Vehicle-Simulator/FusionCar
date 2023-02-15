using System;
using UnityEngine;

namespace VehiclePhysics
{
    [Serializable]
    public struct Wheel
    {
        [Serializable]
        private struct Friction
        {
            public float DynamicCoefficientOfFriction; //Cf
            public float MaxDynamicFriction; //Cfmax
            public float FrictionAsymptote;
            public float AsymptotePoint => 1 / DynamicCoefficientOfFriction;
        }

        [SerializeField] private bool _inflated;
        [SerializeField] private float _wheelRadius;
        [SerializeField] private float _rimRadius;
        [SerializeField] private Friction _forwardFriction;
        [SerializeField] private Friction _sidewaysFriction;

        private Vector3 _lastSuspensionPosition;

        public float Radius => _inflated ? _wheelRadius : _rimRadius;
        public bool Slipping { get; private set; }


        public void ApplyFriction(Transform suspensionTransform, Rigidbody rigidbody, float normalForce,
            Vector3 groundPoint)
        {
            if (normalForce <= 0)
            {
                Slipping = false;
                return;
            }

            var a = suspensionTransform.InverseTransformPoint(_lastSuspensionPosition);
            (float sideways, float forward) slip = (a.x, 0);
            _lastSuspensionPosition = suspensionTransform.position;
            var frictionSideways = CalculateFriction(_sidewaysFriction, slip.sideways) * normalForce;
            var frictionForward = CalculateFriction(_forwardFriction, slip.forward) * normalForce;
            var right = suspensionTransform.right;
            var forward = suspensionTransform.forward;
            var frictionForceLeftVector =
                frictionSideways * right +
                frictionForward * forward;
            rigidbody.AddForceAtPosition(frictionForceLeftVector, groundPoint,
                ForceMode.Acceleration);
        }

        private float CalculateFriction(Friction friction, float slip)
        {
            float calculatedFriction = Mathf.Sign(slip);
            slip = Mathf.Abs(slip);
            Slipping = slip > friction.AsymptotePoint;
            if (Slipping)
            {
                calculatedFriction *= friction.FrictionAsymptote;
            }
            else
            {
                calculatedFriction *= friction.MaxDynamicFriction *
                                      (1 - (friction.DynamicCoefficientOfFriction * slip - 1) *
                                          (friction.DynamicCoefficientOfFriction * slip - 1));
            }

            return calculatedFriction;
        }
    }
}