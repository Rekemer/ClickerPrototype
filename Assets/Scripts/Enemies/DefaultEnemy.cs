using Core;
using ScriptableObjects;
using UnityEngine;

namespace Enemies
{
    public class DefaultEnemy : BaseEnemy
    {
        [SerializeField] private EnemyScore _score;

        protected override void Start()
        {
            base.Start();
            if (!_score)
            {
                Debug.LogWarning(gameObject.name + " doesnt have score set up ");
            }
        }

        public override float GetPoints()
        {
            return _score.score;
        }
    }
}