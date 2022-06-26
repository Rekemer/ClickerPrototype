using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem current;
        public delegate void MyDelegate(int totalMonstersAmount);
        public delegate void MyDelegate2(float timeOfWaiting);
        public MyDelegate  OnEnemiesSpawn;
        public event MyDelegate2  OnUsingBooster;

        private void Awake()
        {
            current = this;
        }

        public void OnSpawnEnemies(int total)
        {
            OnEnemiesSpawn?.Invoke(total);
        }

        public void OnUsingFreezingBooster(float time)
        {
            OnUsingBooster?.Invoke(time);
        }
    }
}