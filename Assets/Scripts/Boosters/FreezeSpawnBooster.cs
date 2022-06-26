using System;
using UnityEngine;
using UnityEngine.EventSystems;
using EventSystem = DefaultNamespace.EventSystem;

class FreezeSpawnBooster : Booster
{
    [SerializeField] private float timeOfFreezing;
    protected override void ApplyBooster()
    {
      EventSystem.current.OnUsingFreezingBooster(timeOfFreezing);
    }
}