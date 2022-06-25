using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
   void Spawn();
}

public class BonusSpawn : MonoBehaviour,ISpawn
{
   public void Spawn()
   {
      throw new System.NotImplementedException();
   }
}