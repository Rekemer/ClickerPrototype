using UnityEngine;
using EventSystem = DefaultNamespace.EventSystem;

namespace Boosters
{
    class FreezeSpawnBooster : Booster
    {
        [SerializeField] private float timeOfFreezing;
        protected override void ApplyBooster()
        {
            EventSystem.current.OnUsingFreezingBooster(timeOfFreezing);
            if (_audio != null)
            {
                _audio.Play();
            }
        }
    }
}