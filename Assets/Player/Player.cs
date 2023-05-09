using Fusion;
using UnityEngine;

namespace Player
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private int _maxTorque;
        [SerializeField] private int _brakingPower;
        [SerializeField] private int _maxSteerAngle;
        
        [Networked] private NetworkButtons PreviousButton { get; set; }

        public override void Spawned()
        {
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
        }


        private void OnInputUpdated(PlayerInput playerInput)
        {
        }
    }
}