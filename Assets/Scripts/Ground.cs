using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private GameObject _prefabGroundTile;
    [SerializeField] private List<BaseSpawn> _spawns;
    private List<BaseEnemy> _currentEnemies= new List<BaseEnemy>();
    private List<BaseEnemy> _deadEnemies= new List<BaseEnemy>();
    private Vector2 _groundCenter;
    public int GroundHeight => _height;
    public int GroundWidth => _width;
    public int CountOfEnemies => _currentEnemies.Count;
    public List<BaseEnemy> CurrentEnemies => _currentEnemies;
    public Vector2 GroundCenter => _groundCenter;
    // Start is called before the first frame update
    void Start()
    {
        if (_width != 0 || _height != 0)
        {
            _groundCenter = new Vector2(_width/2, _height/2);
            GenerateGround();
        }
        else
        {
            Debug.LogError("GROUND: Width and Height are not set up");
        }

        StartSpawning();

    }

    private void LateUpdate()
    {
        foreach (var enemy in _currentEnemies)
        {
            if (enemy.GetState() == State.DEAD)
            {
                _deadEnemies.Add(enemy);
            }
        }

        for (int i = 0; i < _deadEnemies.Count; i++)
        {
            Destroy(_deadEnemies[i].gameObject);   
        }
        _deadEnemies.Clear();
    }

    public  Vector3 GetRandomPosition()
    {
        float randomX = UnityEngine.Random.value;
        float randomZ = UnityEngine.Random.value;
        float xCoord = randomX * _width;
        float zCoord = randomZ * _height;
        Vector3 newPos = new Vector3(xCoord, 0.75f, zCoord);
        return newPos;

    }
    public void AddEnemy(BaseEnemy enemy)
    {
        _currentEnemies.Add(enemy);
        EventSystem.current.OnSpawnEnemies(CountOfEnemies);
    }

    public void EraseEnemy(BaseEnemy enemy)
    {
        if (_currentEnemies.Contains(enemy))
        {
            _currentEnemies.Remove(enemy);
        }
        
    }
    void StartSpawning()
    {
        StartCoroutine(StartSpawningRoutine());
    }

    private IEnumerator StartSpawningRoutine()
    {
        // spawn enemy or items
        foreach (var spawn in _spawns)
        {
            // add him to list
            spawn.Spawn(this);
        }
       
        
        // apply difficulty when certain time is passed (speed of spawning, speed and health of monsters)
        yield return null;
    }

    private void GenerateGround()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                var groundTilePos = new Vector3(j, -0.25f, i);
                GameObject groundTile = Instantiate(_prefabGroundTile, groundTilePos, Quaternion.identity);
                groundTile.name = "groundTile " + i + " " + j;
                groundTile.transform.parent = transform;
            }
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
