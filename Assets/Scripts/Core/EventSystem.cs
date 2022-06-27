using System;
using UnityEngine;

namespace Core
{
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Current;

        public delegate void MyDelegate(int totalMonstersAmount);

        public delegate void MyDelegate2(float timeOfWaiting);

        public MyDelegate OnEnemiesAmountChanged;
        public MyDelegate OnEnemiesDeath;
        public event MyDelegate2 OnUsingBooster;
        public event Action OnNoMoreEnemiesAreLeft;
        public event Action OnEnemiesTooMany;

        private void Awake()
        {
            Current = this;
        }

        public void OnEnemiesChanged(int total)
        {
            OnEnemiesAmountChanged?.Invoke(total);
        }

        public void OnEnemyDeath(int score)
        {
            OnEnemiesDeath?.Invoke(score);
        }

        public void OnEnemiesAreDefeated()
        {
            OnNoMoreEnemiesAreLeft?.Invoke();
        }

        public void OnPlayerDefeated()
        {
            OnEnemiesTooMany?.Invoke();
        }

        public void OnUsingFreezingBooster(float time)
        {
            OnUsingBooster?.Invoke(time);
        }
    }
}