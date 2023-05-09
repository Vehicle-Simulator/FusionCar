using Network;
using UnityEngine;

namespace CarSample
{
    public class VehiclePhysicsClientBehaviour : ClientBehaviour
    {
        [SerializeField] private Transform _cameraTarget;

        public override void Spawned()
        {
            base.Spawned();
            if (Object.HasInputAuthority == false) return;
            SceneContext.Instance.VirtualCamera.Follow = _cameraTarget;
        }
    }
}