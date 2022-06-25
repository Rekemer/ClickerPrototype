using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Field
{
    public int x;
    public int y;
    public int height;
    public int width;

    public Field(int x, int y, int height, int width)
    {
        this.x = x;
        this.y = y;
        this.height = height;
        this.width = width;
    }
}
public abstract class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Field allowedArea;
    private Ground _ground;

    [SerializeField] DifficultyEnemySettings _enemySettings;
    private bool _isMoving;

    private void Awake()
    {
        Ground ground = FindObjectOfType<Ground>();
        
    }

    void Start()
    {
        var height = _ground.GroundHeight;
        var width = _ground.GroundWidth;
        allowedArea = new Field(0, 0, height, width);
        Move();
    }

    protected virtual Vector3 GetRandomPosition()
    {
        float randomX = UnityEngine.Random.value;
        float randomZ = UnityEngine.Random.value;
        float xCoord = randomX * allowedArea.width;
        float zCoord = randomZ * allowedArea.height;
        Vector3 newPos = new Vector3(xCoord, 0, zCoord);
        return newPos;

    }
    
    protected virtual void Move()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        _isMoving = true;
        float time;
        while (_isMoving)
        {
            Vector3 newPos = GetRandomPosition();
            time = 0;
            while ((transform.position - newPos).sqrMagnitude < Vector3.kEpsilon)
            {
                time += Time.deltaTime;
                float speed = _enemySettings.difficultySpeedCurve.Evaluate(time);
                Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            }
            yield return new WaitForSeconds(_enemySettings.timeOfWaitingAfterReachingNewPos);
        }
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DefaultEnemy : BaseEnemy
{
    
}
