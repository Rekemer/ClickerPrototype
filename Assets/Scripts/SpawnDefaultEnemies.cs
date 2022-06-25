using UnityEngine;

public class SpawnDefaultEnemies : BaseSpawn
{
    public override void Spawn(Ground ground)
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(objectToSpawn, ground.GetRandomPosition(),
                Quaternion.LookRotation(new Vector3(-0.5f, 0f, -0.5f), Vector3.up));
        }
        
    }
}