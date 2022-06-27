using System.Collections.Generic;
using Core;

namespace Boosters
{
    class KillAllBooster : Booster
    {
        protected override void ApplyBooster()
        {
            List<BaseEnemy> enemies = _ground.CurrentEnemies;
            if (_audio != null)
            {
                _audio.Play();
            }
            
            foreach (var enemy in enemies)
            {
                enemy.GetDamage(100);
            }
        
        }
    }
}