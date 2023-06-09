using CarSample.Scripts.Player;
using Fusion;
using UnityEngine;

namespace CarSample.Scripts.VehiclePhysics
{
    public class VehiclePhysics : NetworkBehaviour
    {
        [SerializeField] private Axle[] _axles;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _enginePower;


        public override void Spawned()
        {
            foreach (var axle in _axles)
            {
                axle.Initialize();
            }
        }

        public void UpdateFromInput(PlayerCarInputNetworkStruct input)
        {
            foreach (var axle in _axles)
            {
                axle.UpdateAxle(_rigidbody, input.SteeringValue, input.Acceleration * _enginePower);
            }
        }
    }
}