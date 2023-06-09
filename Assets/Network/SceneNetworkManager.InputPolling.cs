using Fusion;
using Player;
using UnityEngine;

namespace Network
{
    public partial class SceneNetworkManager
    {
        private PlayerInput _myInput;

        private void OnEnable()
        {
            if (_runner != null)
            {
                _runner.AddCallbacks(this);
            }
        }

        private void Update()
        {
            _myInput.NetworkButtons.Set(PlayerInputMapper.HandBrake, Input.GetKey(KeyCode.Space));
            _myInput.Acceleration = Input.GetAxis("Vertical");
            _myInput.SteeringValue = Input.GetAxis("Horizontal");
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(_myInput);
            _myInput = default;
        }

        private void OnDisable()
        {
            if (_runner != null)
            {
                _runner.RemoveCallbacks(this);
            }
        }
    }
}