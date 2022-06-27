using System.Collections;
using System.Collections.Generic;
using Core;
using ScriptableObjects;
using UnityEngine;

namespace Tactics
{
    public class DefaultTactics : EnemyTactics
    {
        [SerializeField] private DefaultDifficultySpawnSettings _difficultySpawnSettings;
        private List<Difficulty> _difficulties;
        private GameObject _objectToSpawn;
        private Ground _ground;
        private float _timeSinceSpawningStarted = 0;
        private int _currentDifficulty;
        private float _freezingTime;

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

            EventSystem.Current.OnUsingBooster += SetFreezingTime;
        }

        public override void StartSpawning(Ground ground, GameObject objectToSpawn)
        {
            if (ground == null || objectToSpawn == null || objectToSpawn == null && ground == null)
            {
                Debug.LogWarning("DefaultTactics: Ground or objectToSpawn was not initialized in spawner");
                return;
            }

            _objectToSpawn = objectToSpawn;
            _ground = ground;
            StartCoroutine(StartSpawningRoutine());
        }

        private void SetFreezingTime(float time)
        {
            _freezingTime = time;
            Invoke("UnsetFreezingTime", _freezingTime);
        }

        private void UnsetFreezingTime()
        {
            _freezingTime = 0;
        }

        public IEnumerator StartSpawningRoutine()
        {
            //int iter = 0;
            _currentDifficulty = 0;
            var timeBetweenSpawns = _difficultySpawnSettings.timeBetweenSpawns;
            var currentTime = 0f;
            while (true)
            {
                currentTime += Time.deltaTime;
                _timeSinceSpawningStarted += Time.deltaTime;
                if (_currentDifficulty < _difficulties.Count - 1)
                {
                    if (_timeSinceSpawningStarted > _difficulties[_currentDifficulty].time)
                    {
                        _currentDifficulty++;
                        timeBetweenSpawns *= _difficultySpawnSettings.timeBetweenSpawnsMultiplier;
                    }
                }


                if (currentTime > timeBetweenSpawns)
                {
                    currentTime = 0f;
                    var pos = _ground.GetRandomPosition();
                    var spawnedObject = Instantiate(_objectToSpawn, pos,
                        Quaternion.LookRotation(new Vector3(-0.5f, 0f, -0.5f), Vector3.up));
                    // spawn enemies with correct settings
                    var enemy = spawnedObject.GetComponent<BaseEnemy>();
                    if (enemy)
                    {
                        enemy.SetHealth(_difficulties[_currentDifficulty].enemyHealth);
                        enemy.SetSpeed(_difficulties[_currentDifficulty].enemySpeed);
                        enemy.SetTimeOfWaiting(_difficulties[_currentDifficulty].timeOfWaitingAfretReachingNewPos);
                    }
                }

                // check exit condition
                if (_ground.CountOfEnemies >= _ground.GetMaxEnemies())
                {
                    EventSystem.Current.OnPlayerDefeated();
                }

                yield return new WaitForSeconds(_freezingTime);
            }
        }

        private void OnDisable()
        {
            EventSystem.Current.OnUsingBooster -= SetFreezingTime;
        }
    }
}