using System.Collections.Generic;
using Fusion;
using UnityEngine;
using VehiclePhysics;

namespace Player
{
    public class Player : NetworkBehaviour
    {
      

        [SerializeField] private AnimationCurve _torqueCurve;
        [SerializeField] private int _maxTorque;
        [SerializeField] private int _brakingPower;
        [SerializeField] private int _maxRpm;
        [SerializeField] private int _maxSteerAngle;
        [SerializeField] private Transform _cameraTarget;


        [Networked] private NetworkButtons PreviousButton { get; set; }
        [Networked] private float Acceleration { get; set; }
        [Networked] private float SteeringAngle { get; set; }

        public override void Spawned()
        {
            if (Object.HasInputAuthority == false) return;
            SceneContext.Instance.VirtualCamera.Follow = _cameraTarget;
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

            if (Runner.Simulation.IsForward)
            {
                var torque = Acceleration * _maxTorque;
                var steerAngle = SteeringAngle * _maxSteerAngle;
               
            }
        }

        public override void Render()
        {
            
        }

        private void OnInputUpdated(PlayerInput playerInput)
        {
            SteeringAngle = playerInput.SteeringValue;
            Acceleration = playerInput.Acceleration;
        }

        private void UpdateAxlePosition(Axle axle)
        {
        }
    }
}