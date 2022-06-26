using Core;
using UnityEngine;

namespace Spawns
{
  public abstract class BaseSpawn: MonoBehaviour
  {
    [SerializeField] protected GameObject objectToSpawn;
    public abstract void Spawn(Ground ground);
  }
}