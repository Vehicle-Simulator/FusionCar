using Fusion;
using Player;
using UnityEngine;

namespace CarSample
{
    public class VehiclePhysics : NetworkBehaviour
    {
        [SerializeField] private Axle[] _axles;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _enginePower;


        [Networked] private NetworkButtons PreviousButton { get; set; }


        public override void Spawned()
        {
            foreach (var axle in _axles)
            {
                axle.Initialize();
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput<PlayerInput>(out var input))
            {
                var pressed = input.NetworkButtons.GetPressed(PreviousButton);
                var released = input.NetworkButtons.GetReleased(PreviousButton);

                PreviousButton = input.NetworkButtons;
                OnInputUpdated(input);
            }

            foreach (var axle in _axles)
            {
                axle.UpdateAxle(_rigidbody, input.SteeringValue, input.Acceleration * _enginePower);
            }
        }


        private void OnInputUpdated(PlayerInput playerInput)
        {
        }
    }
}