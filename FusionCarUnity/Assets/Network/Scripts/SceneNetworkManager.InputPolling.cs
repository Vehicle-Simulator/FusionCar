using Fusion;
using Player;
using UnityEngine;

namespace Network
{
    public partial class SceneNetworkManager
    {
        private PlayerInput _myInput;

        private void Update()
        {
            _myInput.NetworkButtons.Set(PlayerInputMapper.HandBrake, Input.GetKey(KeyCode.Space));
            _myInput.Acceleration = Input.GetAxis("Vertical");
            _myInput.SteeringValue = Input.GetAxis("Horizontal");
        }
    }
}