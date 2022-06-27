using UnityEngine;
using EventSystem = Core.EventSystem;

namespace Boosters
{
    class FreezeSpawnBooster : Booster
    {
        [SerializeField] private float timeOfFreezing;

        protected override void ApplyBooster()
        {
            EventSystem.Current.OnUsingFreezingBooster(timeOfFreezing);
            if (_audio != null)
            {
                _audio.Play();
            }
        }
    }
}