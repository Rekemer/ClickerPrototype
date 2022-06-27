using UnityEngine;

namespace Core
{
    public abstract class BaseSpawn : MonoBehaviour
    {
        [SerializeField] protected GameObject objectToSpawn;
        public abstract void Spawn(Ground ground);
    }
}