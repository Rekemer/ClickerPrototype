﻿using UnityEngine;

public class SpawnDefaultEnemies : BaseSpawn
{
    [SerializeField] private EnemyTactics _enemyTactics;

    public override void Spawn(Ground ground)
    {
        _enemyTactics.StartSpawning(ground, objectToSpawn);
    }
}