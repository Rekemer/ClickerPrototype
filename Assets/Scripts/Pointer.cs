using UnityEngine;

namespace DefaultNamespace
{
    public class Pointer : MonoBehaviour
    {
        private BaseEnemy _enemyToPointTo;

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