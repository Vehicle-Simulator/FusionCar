using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace VehiclePhysics
{
    [Serializable]
    public struct Wheel
    {
        [Serializable]
        private struct Friction
        {
            public float MaxStaticFriction;
            public float DynamicCoefficientOfFriction; //Cf
            public float MaxDynamicFriction; //Cfmax
            public float FrictionAsymptote;
            [HideInInspector] public float StaticFriction;
            public float AsymptotePoint => 1 / DynamicCoefficientOfFriction;
        }

        [SerializeField] private bool _inflated;
        [SerializeField] private float _wheelRadius;
        [SerializeField] private float _rimRadius;
        [SerializeField] private Friction _forwardFriction;
        [SerializeField] private Friction _sidewaysFriction;

        private Vector3 _lastSuspensionPosition;

        public float Radius => _inflated ? _wheelRadius : _rimRadius;


        public (float sideways, float forward) ApplyFriction(Transform suspensionTransform, float normalForce)
        {
            var a = suspensionTransform.InverseTransformPoint(_lastSuspensionPosition);
            (float sideways, float forward) slip = (a.x, a.z);
            _lastSuspensionPosition = suspensionTransform.position;
            float x = CalculateFriction(_sidewaysFriction, slip.sideways);
            float y = CalculateFriction(_forwardFriction, slip.forward);
            return (x * normalForce, y * normalForce);
        }

        private float CalculateFriction(Friction friction, float slip)
        {
            float calculatedFriction = Mathf.Sign(slip);
            slip = Mathf.Abs(slip);
            var acceleration = slip / (Time.fixedDeltaTime * Time.fixedDeltaTime);
            acceleration += friction.StaticFriction;
            friction.StaticFriction = acceleration;
            friction.StaticFriction = Mathf.Min(friction.StaticFriction, friction.MaxStaticFriction);
            if (slip > friction.AsymptotePoint)
            {
                calculatedFriction *= friction.FrictionAsymptote;
            }
            else
            {
                calculatedFriction *= friction.MaxDynamicFriction *
                                      (1 - (friction.DynamicCoefficientOfFriction * slip - 1) *
                                          (friction.DynamicCoefficientOfFriction * slip - 1)) +
                                      friction.StaticFriction;
            }

            return calculatedFriction;
        }
    }
}