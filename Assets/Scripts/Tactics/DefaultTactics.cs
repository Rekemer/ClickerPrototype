using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTactics : EnemyTactics
{
    [SerializeField] private DefaultDifficultySpawnSettings _difficultySpawnSettings;
    private List<Difficulty> _difficulties;
    private GameObject _objectToSpawn;
    private Ground _ground;
    private float timeSinceSpawningStarted =0;
    private int currentDifficulty;

    public void Start()
    {
        _difficulties = _difficultySpawnSettings.difficulties;
        if (_difficulties == null)
        {
            Debug.LogWarning("DefaultDifficultySpawnSettings are not initialized");
            return;
        }
        for (int i = 0; i < _difficulties.Count - 1; i++)
        {
            if (_difficulties[i].time > _difficulties[i + 1].time)
            {
                Debug.LogWarning("DefaultDifficultySpawnSettings: time  must be sorted in ascending order");
            }
        }
    }

    public override void StartSpawning(Ground ground,GameObject objectToSpawn )
    {
        if ( ground == null || objectToSpawn == null ||  objectToSpawn == null && ground == null)
        {
            Debug.LogWarning("DefaultTactics: Ground or objectToSpawn was not initialized in spawner");
            return;
        }

        _objectToSpawn = objectToSpawn;
        _ground = ground;
        StartCoroutine(StartSpawningRoutine());
    }

    public IEnumerator StartSpawningRoutine()
    {
        //int iter = 0;
        currentDifficulty = 0;
        var timeBetweenSpawns = _difficultySpawnSettings.timeBetweenSpawns;
        var currentTime = 0f;
        while (true)
        {
          //  iter++;
            currentTime+=Time.deltaTime;
            timeSinceSpawningStarted += Time.deltaTime;
            // if (iter > 1000)
            // {
            //     break;
            // }

            // change difficulty
            if (currentDifficulty < _difficulties.Count - 1)
            {
                if (timeSinceSpawningStarted > _difficulties[currentDifficulty].time)
                {
                    currentDifficulty++;
                   
                }
            }
            

            if (currentTime > timeBetweenSpawns)
            {
                currentTime = 0f;
                var pos = _ground.GetRandomPosition();
                 var spawnedObject = Instantiate(_objectToSpawn,pos, 
                    Quaternion.LookRotation(new Vector3(-0.5f, 0f, -0.5f), Vector3.up));
                // spawn enemies with correct settings
                
                var enemy = spawnedObject.GetComponent<BaseEnemy>();
                if (enemy)
                {
                    enemy.SetHealth(_difficulties[currentDifficulty].enemyHealth);
                    enemy.SetSpeed(_difficulties[currentDifficulty].enemySpeed);
                }
             
                
            }
            // check exit condition
            yield return null;
        }
        yield return null;
    }
}