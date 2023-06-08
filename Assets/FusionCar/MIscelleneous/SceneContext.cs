using Cinemachine;
using UnityEngine;

namespace FusionCar.Miscelleneous
{
    public class SceneContext : MonoBehaviour
    {
        public static SceneContext Instance;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        public CinemachineVirtualCamera VirtualCamera => _virtualCamera;
        public CinemachineBrain CinemachineBrain => _cinemachineBrain;

        private void Start()
        {
            Instance = this;
        }

        public void SwitchCamera(CinemachineVirtualCamera virtualCamera)
        {
        }
    }
}