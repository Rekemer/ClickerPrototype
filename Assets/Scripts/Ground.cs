using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private GameObject _prefabGroundTile;
    private List<BaseEnemy> _currentEnemies= new List<BaseEnemy>();
    private Vector2 _groundCenter;
    public int GroundHeight => _height;
    public int GroundWidth => _width;
    
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
        
        
    }

    void StartSpawning()
    {
        StartCoroutine(StartSpawningRoutine());
    }

    private IEnumerator StartSpawningRoutine()
    {
        // spawn enemy or items
        
        // add him to list
        
        // apply difficulty when certain time is passed (speed of spawning, speed and health of monsters)
        yield return null;
    }

    private void GenerateGround()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                var groundTilePos = new Vector3(j, 0, i);
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
