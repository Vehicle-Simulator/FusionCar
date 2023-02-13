using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace VehiclePhysics
{
#if UNITY_EDITOR
    public class SuspensionBehaviour : MonoBehaviour, IDisposable
    {
        [SerializeField] private Suspension _suspension;
        [SerializeField] private Wheel _wheel;

        public Wheel Wheel => _wheel;
        public Suspension Suspension => _suspension;

        private void OnValidate()
        {
            GetComponentInParent<DriveTrain>().RefreshData();
        }

        public void Dispose()
        {
            Destroy(GetComponent<SuspensionBehaviour>());
        }
    }
#endif
}