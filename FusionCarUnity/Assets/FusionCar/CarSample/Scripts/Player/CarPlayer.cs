using System.Collections.Generic;
using Cinemachine;
using Example;
using Fusion;
using FusionCar.Miscelleneous;
using Unity.Mathematics;
using UnityEngine;

namespace CarSample.Scripts.Player
{
    public class CarPlayer : NetworkBehaviour, IControllable
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private VehiclePhysics.VehiclePhysics _vehiclePhysics;
        [SerializeField] private List<Transform> _seats;
        [SerializeField] private List<Transform> _exits;

        [Networked] private NetworkButtons PreviousButton { get; set; }
        [Networked] private PlayerRef InteractingPlayer { get; set; }

        private PlayerInteraction _playerInteraction;

        private void Update()
        {
            _camera.SetActive(HasInputAuthority);
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput<PlayerCarInputNetworkStruct>(out var input))
            {
                var pressed = input.NetworkButtons.GetPressed(PreviousButton);
                var released = input.NetworkButtons.GetReleased(PreviousButton);

                PreviousButton = input.NetworkButtons;

                if (pressed.IsSet(PlayerInputMapper.Exit))
                {
                    ExitCar();
                }
            }

            UpdateVehiclePhysics(input);
        }

        private void ExitCar()
        {
            var playerTransform = _playerInteraction.transform;
            playerTransform.SetParent(null);
            _playerInteraction.Player.SetParentedMode(false, _exits[0].transform.position,
                _exits[0].transform.rotation);
            if (HasStateAuthority)
            {
                _playerInteraction.ExitInteraction();
                Object.RemoveInputAuthority();
                _playerInteraction = null;
                InteractingPlayer = PlayerRef.None;
            }
        }

        private void UpdateVehiclePhysics(PlayerCarInputNetworkStruct input)
        {
            _vehiclePhysics.UpdateFromInput(input);
        }

        public bool Interact(PlayerInteraction playerInteraction)
        {
            if (InteractingPlayer != PlayerRef.None)
            {
                return false;
            }

            if (HasStateAuthority)
            {
                var inputAuthority = playerInteraction.Object.InputAuthority;
                Object.AssignInputAuthority(inputAuthority);
                InteractingPlayer = inputAuthority;
            }

            _playerInteraction = playerInteraction;
            var playerTransform = _playerInteraction.transform;
            playerTransform.SetParent(_seats[0]);
            playerTransform.localPosition = Vector3.zero;
            playerTransform.localEulerAngles = Vector3.zero;
            _playerInteraction.Player.SetParentedMode(true, Vector3.zero, quaternion.identity);
            return true;
        }
    }
}