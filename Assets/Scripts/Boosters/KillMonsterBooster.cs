﻿using Core;
using UnityEngine;

namespace Boosters
{
    class KillMonsterBooster : Booster
    {
        protected override void ApplyBooster()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                var enemy = hit.transform.GetComponent<BaseEnemy>();
                if (enemy)
                {
                    enemy.SetState(State.DEAD);
                    if (_audio != null)
                    {
                        _audio.Play();
                    }
                }
            }
        }
    }
}