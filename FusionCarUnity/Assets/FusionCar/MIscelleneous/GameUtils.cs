using UnityEngine;

namespace FusionCar.Miscelleneous
{
    public static class GameUtils
    {
        public static void SetActive(this Component component, bool active)
        {
            component.gameObject.SetActive(active);
        }
    }
}