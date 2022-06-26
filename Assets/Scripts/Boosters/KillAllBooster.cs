using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

class KillAllBooster : Booster
{
    protected override void ApplyBooster()
    {
        List<BaseEnemy> enemies = _ground.CurrentEnemies;
        for (int i=0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetDamage(100);
            }
           
        }
        enemies.Clear();
    }
}