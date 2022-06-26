using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTactics : EnemyTactics
{
    [SerializeField]
    private WaveDifficultySpawnSettings _difficultySpawnSettings;
    private List<Difficulty> _difficulties;
    private GameObject _objectToSpawn;
    private Ground _ground;
    private float timeSinceSpawningStarted =0;
    private int _currentWave;

    private void Start()
    {
        _difficulties = _difficultySpawnSettings.difficulties;
        if (_difficulties == null)
        {
            Debug.LogError("DefaultDifficultySpawnSettings are not initialized");
            return;
        }
    }

    public override void StartSpawning(Ground ground, GameObject objectToSpawn)
    {
        if ( ground == null || objectToSpawn == null ||  objectToSpawn == null && ground == null)
        {
            Debug.LogWarning("WaveTactics: Ground or objectToSpawn was not initialized in spawner");
            return;
        }
        _objectToSpawn = objectToSpawn;
        _ground = ground;
        StartCoroutine(StartSpawningRoutine());
    }

    private bool AreThereEnemies()
    {
        return _ground.CountOfEnemies > 0 ? true : false;
    }
    IEnumerator StartSpawningRoutine()
    {
        int currentWave = 0;
        var timeSinceSpawningStarted = 0f;
        var amountsOfEnemiesPerWave = _difficultySpawnSettings.amountOfEnemiesPerWave;
        while (true)
        {
            timeSinceSpawningStarted += Time.deltaTime;
            // check if there are any enemies - ground 
            if (!AreThereEnemies())
            {
                if (currentWave < _difficulties.Count - 1)
                {
                    // spawn enemies according to the wave
                    int amountOfEnemies = amountsOfEnemiesPerWave[this._currentWave];
                    // Debug.Log(_currentWave);
                    SpawnWave(amountOfEnemies);
                    //
                }
                else
                {
                    //  or there are no more waves left
                }
                
            }
           
            
            yield return null;
        }
        
    }

    private void SpawnWave(int amountOfEnemies)
    {
        StartCoroutine(SpawnWaveRoutine(amountOfEnemies));
    }

    IEnumerator SpawnWaveRoutine(int amountOfEnemies)
    {
        int currAmount = 0;
        while (currAmount < amountOfEnemies)
        {
            var pos = _ground.GetRandomPosition();
            var spawnedObject = Instantiate(_objectToSpawn,pos, 
                Quaternion.LookRotation(new Vector3(-0.5f, 0f, -0.5f), Vector3.up));
            // spawn enemies with correct settings
            var enemy = spawnedObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.SetHealth(_difficulties[_currentWave].enemyHealth);
                enemy.SetSpeed(_difficulties[_currentWave].enemySpeed);
            }

            currAmount++;
            yield return new WaitForSeconds(_difficultySpawnSettings.timeBetweenSpawns);
        }

        _currentWave++;

    }
}