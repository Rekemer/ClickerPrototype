using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

class KillAllBooster : Booster
{
    protected override void ApplyBooster()
    {
        List<BaseEnemy> enemies = _ground.CurrentEnemies;
        foreach (var enemy in enemies)
        {
            enemy.SetState(State.DEAD);
        }
        
    }
}