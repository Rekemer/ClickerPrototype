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
    private int currentWave;

    private void Start()
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
                Debug.LogWarning("WaveDifficultySpawnSettings: waves must be sorted in ascending order");
            }
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

    IEnumerator StartSpawningRoutine()
    {
        var currentTime = 0f;
        var timeSinceSpawningStarted = 0f;
        var timeBetweenSpans = _difficultySpawnSettings.timeBetweenSpawns;
        while (true)
        {
            timeSinceSpawningStarted += Time.deltaTime;
            currentTime+=Time.deltaTime;
            yield return null;
        }
        
    }
}