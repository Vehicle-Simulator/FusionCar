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
            public float CoefficientOfFriction; //Cf
            public float CoefficientOfFrictionMax; //Cfmax
            public float FrictionAsymptote; 
        }

        [SerializeField] private bool _inflated;
        [SerializeField] private float _wheelRadius;
        [SerializeField] private float _rimRadius;
        [SerializeField] private Friction _forwardFriction;
        [SerializeField] private Friction _sidewaysFriction;

        public float Radius => _inflated ? _wheelRadius : _rimRadius;


        public (float sideways, float forward) CalculateFriction((float sideways, float forward) slip)
        {
            var x = _sidewaysFriction.CoefficientOfFrictionMax *
                    (1 - (_sidewaysFriction.CoefficientOfFriction * slip.sideways - 1) * (_sidewaysFriction.CoefficientOfFriction * slip.sideways - 1));//sideways frtn
            var y = _forwardFriction.CoefficientOfFrictionMax *
                    (1 - (_forwardFriction.CoefficientOfFriction * slip.forward - 1) *
                        (_forwardFriction.CoefficientOfFriction * slip.forward - 1));
            return (x, y);
            
            //equation:: frtn= Cfmax(1-(Cf*slip-1)^2)
        }
    }
}