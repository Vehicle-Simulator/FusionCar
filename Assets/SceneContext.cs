using Cinemachine;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    public static SceneContext Instance;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    public CinemachineVirtualCamera VirtualCamera => _virtualCamera;

    private void Start()
    {
        Instance = this;
    }
}