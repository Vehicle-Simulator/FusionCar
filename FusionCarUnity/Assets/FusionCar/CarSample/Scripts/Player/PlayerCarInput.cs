using Fusion;
using UnityEngine;

namespace CarSample.Scripts.Player
{
    public class PlayerCarInput : NetworkBehaviour
    {
        private PlayerCarInputNetworkStruct _myCarInputNetworkStruct;
        private NetworkEvents NetworkEvents { get; set; }

        public override void Spawned()
        {
            base.Spawned();
            NetworkEvents = Runner.GetComponent<NetworkEvents>();
            NetworkEvents.OnInput.AddListener(OnInput);
        }

        private void Update()
        {
            if (HasInputAuthority == false) return;
            _myCarInputNetworkStruct.NetworkButtons.Set(PlayerInputMapper.HandBrake, Input.GetKey(KeyCode.Space));
            _myCarInputNetworkStruct.NetworkButtons.Set(PlayerInputMapper.Exit, Input.GetKey(KeyCode.F));
            _myCarInputNetworkStruct.Acceleration = Input.GetAxis("Vertical");
            _myCarInputNetworkStruct.SteeringValue = Input.GetAxis("Horizontal");
        }

        private void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (HasInputAuthority == false) return;
            input.Set(_myCarInputNetworkStruct);
            _myCarInputNetworkStruct = default;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            NetworkEvents.OnInput.RemoveListener(OnInput);
            NetworkEvents = null;
            base.Despawned(runner, hasState);
        }
    }
}