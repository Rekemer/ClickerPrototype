using Core;
using UnityEngine;

namespace Tactics
{
    public abstract class EnemyTactics : MonoBehaviour
    {
        public abstract void StartSpawning(Ground ground, GameObject objectToSpawn);
    }
}