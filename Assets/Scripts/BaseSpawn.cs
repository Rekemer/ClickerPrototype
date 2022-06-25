using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn: MonoBehaviour
{
  [SerializeField] protected GameObject objectToSpawn;
  public abstract void Spawn(Ground ground);
}