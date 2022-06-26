using UnityEngine;

public class SpawnStrongerEnemies :BaseSpawn
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private EnemyTactics _enemyTactics;
    public override void Spawn(Ground ground)
    {
        _enemyTactics.StartSpawning(ground,objectToSpawn);
    }
}