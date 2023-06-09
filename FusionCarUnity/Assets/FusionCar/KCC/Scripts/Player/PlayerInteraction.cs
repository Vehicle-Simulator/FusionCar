using Fusion;
using FusionCar.Miscelleneous;
using UnityEngine;

namespace Example
{
    public class PlayerInteraction : NetworkBehaviour
    {
        [SerializeField] private Player _player;
        private IInteractable _interactingBehaviour;

        public Player Player => _player;

        private void OnTriggerEnter(Collider other)
        {
            var monoBehaviour = other.GetComponent<IInteractable>();
            if (monoBehaviour != null)
            {
                _interactingBehaviour = monoBehaviour;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var monoBehaviour = other.GetComponent<IInteractable>();
            if (monoBehaviour != null && _interactingBehaviour == monoBehaviour)
            {
                _interactingBehaviour = null;
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput<GameplayInput>(out var input))
            {
                if (input.RMB)
                {
                    Interact();
                }
            }
        }

        private void Interact()
        {
            if (_interactingBehaviour == null) return;
            Debug.Log($"{nameof(PlayerInteraction)}::Interacting with {_interactingBehaviour}");

            bool interacting = _interactingBehaviour.Interact(this);
            if (_interactingBehaviour is IControllable controllable)
            {
                _player.Input.IgnoreInput = interacting;
            }
        }

        public void ExitInteraction()
        {
            _player.Input.IgnoreInput = false;
        }
    }
}