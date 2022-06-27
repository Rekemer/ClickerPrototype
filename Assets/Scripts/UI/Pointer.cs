using Core;
using UnityEngine;

namespace UI
{
    public class Pointer : MonoBehaviour
    {
        private BaseEnemy _enemyToPointTo;

        private void Awake()
        {
            _enemyToPointTo = null;
        }

        public void SetEnemy(BaseEnemy enemy)
        {
            _enemyToPointTo = enemy;
        }

        public BaseEnemy GetEnemy()
        {
            return _enemyToPointTo;
        }
    }
}